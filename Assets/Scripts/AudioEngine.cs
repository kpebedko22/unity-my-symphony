using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Скрипт, отвечающий за обработку музыки и получения основных необходимых значений
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioEngine : MonoBehaviour {
    /// <summary>
    /// Количество диапазонов частот
    /// </summary>
    public const int FrequencyRangesCount = 8;

    /// <summary>
    /// Количество звуковых фрагментов
    /// </summary>
    public const int SamplesCount = 512;

    /// <summary>
    /// Источник аудио
    /// </summary>
    private AudioSource _audioSource;

    // samples - массив сэмплов (звуковые фрагменты)
    // freqBand - массив диапазонов частот
    // bandBuffer - массив для смягчения визуализации
    // bufferDecrease - массив для смягчения визуализации
    public static float[] samples = new float[SamplesCount];
    private static readonly float[] FreqBand = new float[FrequencyRangesCount];
    public static float[] bandBuffer = new float[FrequencyRangesCount];
    private float[] bufferDecrease = new float[FrequencyRangesCount];

    //
    private float[] freqBandHighest = new float[FrequencyRangesCount];
    public static float[] audioBand = new float[FrequencyRangesCount];
    public static float[] audioBandBuffer = new float[FrequencyRangesCount];

    // амплитуда, буфер амлитуды и наибольшее значение амплитуды
    public static float amplitude, amplitudeBuffer;
    private float amplitudeHighest;

    public Text textAudioTotalTime;
    public Text textAudioPlayedTime;

    private AudioClip _audioClip;

    private void Start() {
        // задаем источник аудио из компонента AudioSource
        _audioSource = GetComponent<AudioSource>();

        _audioClip = StaticStorage.AudioClip;

        if (!_audioClip) {
            Debug.Log("Clip is not set.");
            return;
        }

        // задаем аудио для компонента AudioSource
        // включаем воспроизведение аудио
        _audioSource.clip = _audioClip;
        _audioSource.Play();

        textAudioTotalTime.text = TimeFromSeconds(_audioSource.clip.length);
    }

    private string TimeFromSeconds(float seconds) {
        TimeSpan time = TimeSpan.FromSeconds(seconds);

        return $"{time.Minutes} : {time.Seconds}";
    }

    private void Update() {
        if (!_audioClip) {
            return;
        }

        textAudioPlayedTime.text = TimeFromSeconds(_audioSource.time);

        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();

        // если аудио закончило играть, и флаг игры = false
        // то вызываем функцию завершения игры
        // (флаг игры нужен чтобы данное действие произошло только раз,
        // т.к. мы в функции Update)
        if (!_audioSource.isPlaying && !GameController.gameIsOver) {
            Camera.main.GetComponent<GameController>().EndGame();
        }
    }

    private void CreateAudioBands() {
        for (var i = 0; i < FrequencyRangesCount; i++) {
            if (FreqBand[i] > freqBandHighest[i]) {
                freqBandHighest[i] = FreqBand[i];
            }

            audioBand[i] = FreqBand[i] / freqBandHighest[i];
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
        for (var i = 0; i < FrequencyRangesCount; i++) {
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
        _audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    private void BandBuffer() {
        for (var i = 0; i < FrequencyRangesCount; ++i) {
            // если значение диапазона больше 
            if (FreqBand[i] > bandBuffer[i]) {
                bandBuffer[i] = FreqBand[i];
                bufferDecrease[i] = 0.005f;
            }

            // если значение диапазона меньше
            if (FreqBand[i] < bandBuffer[i]) {
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
    /// 20 - 60 Hz (Суббас) <br/>
    /// 60 - 250 Hz (Бас) <br/>
    /// 250 - 500 Hz (Нижний средний диапазон) <br/>
    /// 500 - 2000 Hz (Средний диапазон) <br/>
    /// 2000 - 4000 Hz (Высокий средний диапазон) <br/>
    /// 4000 - 6000 Hz (Присутствие) <br/>
    /// 6000 - 20000 Hz (Высокий) <br/>
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
        var count = 0;

        // Для каждого диапазона вычисляем (average * 10)
        for (var i = 0; i < FrequencyRangesCount; i++) {
            float average = 0;

            // Вычисляем кол-во сэмлов в диапазоне
            var sampleCount = (int)Mathf.Pow(2, i) * 2;

            // Если последний диапазон, то добавляем оставшиеся 2
            if (i == FrequencyRangesCount - 1) {
                sampleCount += 2;
            }

            // Вычисляем average
            for (var j = 0; j < sampleCount; j++) {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            // Сохраняем average увеличивая в 10 раз, т.к. число маленькое
            FreqBand[i] = average * 10;
        }
    }
}