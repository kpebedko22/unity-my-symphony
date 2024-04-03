using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Скрипт, отвечающий за обработку музыки и получения основных необходимых значений
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioEngine : MonoBehaviour {
    /// <summary>
    /// Источник аудио
    /// </summary>
    private AudioSource audioSource;

    // samples - массив сэмплов (звуковые фрагменты)
    // freqBand - массив диапазонов частот
    // bandBuffer - массив для смягчения визуализации
    // bufferDecrease - массив для смягчения визуализации
    public static float[] samples = new float[512];
    public static float[] freqBand = new float[8];
    public static float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];

    //
    private float[] freqBandHighest = new float[8];
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    // амплитуда, буфер амлитуды и наибольшее значение амплитуды
    public static float amplitude, amplitudeBuffer;
    private float amplitudeHighest;

    public Text textAudioTotalTime;
    public Text textAudioPlayedTime;

    private void Start() {
        // задаем аудио для компонента AudioSource
        // которое было выбрано на сцене главного меню MainMenu
        GetComponent<AudioSource>().clip = StaticStorage.AudioClip;

        // задаем источник аудио из компонента AudioSource
        audioSource = GetComponent<AudioSource>();

        // включаем воспроизведение аудио
        audioSource.Play();

        textAudioTotalTime.text = TimeFromSeconds(audioSource.clip.length);
    }

    private string TimeFromSeconds(float seconds) {
        TimeSpan time = TimeSpan.FromSeconds(seconds);

        return $"{time.Minutes} : {time.Seconds}";
    }

    private void Update() {
        textAudioPlayedTime.text = TimeFromSeconds(audioSource.time);

        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();

        // если аудио закончило играть, и флаг игры = false
        // то вызываем функцию завершения игры
        // (флаг игры нужен чтобы данное действие произошло только раз,
        // т.к. мы в функции Update)
        if (!audioSource.isPlaying && !GameController.gameIsOver) {
            Camera.main.GetComponent<GameController>().EndGame();
        }
    }

    private void CreateAudioBands() {
        for (var i = 0; i < 8; i++) {
            if (freqBand[i] > freqBandHighest[i]) {
                freqBandHighest[i] = freqBand[i];
            }

            audioBand[i] = freqBand[i] / freqBandHighest[i];
            audioBandBuffer[i] = bandBuffer[i] / freqBandHighest[i];
        }
    }

    /// <summary>
    /// Вычислияем амплитуду
    /// </summary>
    private void GetAmplitude() {
        // временные переменные
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        // суммируем значения
        for (var i = 0; i < 8; i++) {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }

        // проверяем, если текущая амплитуда больше наибольшей амплитуды
        // то наибольшую амплитуду приравниваем текущей
        if (currentAmplitude > amplitudeHighest) {
            amplitudeHighest = currentAmplitude;
        }

        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }

    private void GetSpectrumAudioSource() {
        // GetSpectrumData
        // Возвращает блок из данных спектра из текущего играющего источника.

        // FFTWindow
        // Спектральный анализ типов окон.
        // Используйте для уменьшения утечек сигналов по частотным диапазонам.
        // FFTWindow.Blackman
        // W[n] = 0.42 - (0.5 * COS(n/N) ) + (0.08 * COS(2.0 * n/N) ).
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    private void BandBuffer() {
        for (var i = 0; i < 8; ++i) {
            // если значение диапазона больше 
            if (freqBand[i] > bandBuffer[i]) {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = 0.005f;
            }

            // если значение диапазона меньше
            if (freqBand[i] < bandBuffer[i]) {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    /// <summary>
    /// <para>
    /// Создание 8 диапазонов частот
    /// <br/>
    /// 22050 / 512 = 43 hertz / sample
    /// </para> 
    /// <para>
    /// 20 - 60 Hz <br/>
    /// 60 - 250 Hz <br/>
    /// 250 - 500 Hz <br/>
    /// 500 - 2000 Hz <br/>
    /// 2000 - 4000 Hz <br/>
    /// 4000 - 6000 Hz <br/>
    /// 6000 - 20000 Hz <br/>
    /// </para>
    /// <para>
    /// 0: 2 = 86 Hz <br/>
    /// 1: 4 = 172 Hz (87 - 258) <br/>
    /// 2: 8 = 344 Hz (259 - 602) <br/>
    /// 3: 16 = 688 Hz (603 - 1290) <br/>
    /// 4: 32 = 1376 Hz (1291 - 2666) <br/>
    /// 5: 64 = 2752 Hz (2667 - 5418) <br/>
    /// 6: 128 = 5504 Hz (5419 - 10922) <br/>
    /// 7: 256 = 11008 Hz (10923 - 21930) <br/>
    /// </para>
    /// <para>510 + 2 будут помещены в 7</para>
    /// </summary>
    private void MakeFrequencyBands() {
        // Номер сэмпла
        int count = 0;

        // Для каждого диапазона вычисляем (average * 10)
        for (int i = 0; i < 8; i++) {
            float average = 0;

            // Вычисляем кол-во сэмлов в диапазоне
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            // Если последний диапазон, то добавляем оставшиеся 2
            if (i == 7) {
                sampleCount += 2;
            }

            // Вычисляем average
            for (int j = 0; j < sampleCount; j++) {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            // Сохраняем average увеличивая в 10 раз, т.к. число маленькое
            freqBand[i] = average * 10;
        }
    }
}