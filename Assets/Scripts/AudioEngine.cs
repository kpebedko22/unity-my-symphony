using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEngine : MonoBehaviour {
	/*
	 * Скрипт, отвечающий за обработку музыки
	 * и получения основных необхоидмых значений
	 */

	// источник аудио
	AudioSource audioSource;

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

	void Start () {

		// задаем аудио для компонента AudioSource
		// которое было выбрано на сцене главного меню MainMenu
		GetComponent<AudioSource>().clip = StaticStorage.audioClip;

		// задаем источник аудио из компонента AudioSource
		audioSource = GetComponent<AudioSource>();

		// включаем воспроизведение аудио
		GetComponent<AudioSource>().Play();
	}
	
	void Update () {
		GetSpectrumAudioSource();
		MakeFrequencyBands();
		BandBuffer();
		CreateAudioBands();
		GetAmplitude();

		// если аудио закончило играть, и флаг игры = false
		// то вызываем функцию завершения игры
		// (флаг игры нужен чтобы данное действие произошло только раз,
		// т.к. мы в функции Update)
		if (!GetComponent<AudioSource>().isPlaying && !GameController.gameIsOver)
			Camera.main.GetComponent<GameController>().EndGame();
	}

	void CreateAudioBands() {
		for (int i = 0; i < 8; i++) {

			if (freqBand[i] > freqBandHighest[i])
				freqBandHighest[i] = freqBand[i];

			audioBand[i] = (freqBand[i] / freqBandHighest[i]);
			audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);
		}
	}

	void GetAmplitude() {
		// вычислияем амплитуду

		// временные переменные
		float currentAmplitude = 0;
		float currentAmplitudeByffer = 0;

		// суммируем значения
		for (int i = 0; i < 8; i++) {
			currentAmplitude += audioBand[i];
			currentAmplitudeByffer += audioBandBuffer[i];
		}

		// проверяем, если текущая амплитуда больше наибольшей амплитуды
		// то наибольшую амплитуду приравниваем текущей
		if (currentAmplitude > amplitudeHighest) amplitudeHighest = currentAmplitude;

		//
		amplitude = currentAmplitude / amplitudeHighest;
		amplitudeBuffer = currentAmplitudeByffer / amplitudeHighest;
	}

	void GetSpectrumAudioSource() {

		// GetSpectrumData
		// Возвращает блок из данных спектра из текущего играющего источника.

		// FFTWindow
		// Спектральный анализ типов окон.
		// Используйте для уменьшения утечек сигналов по частотным диапазонам.
		// FFTWindow.Blackman
		// W[n] = 0.42 - (0.5 * COS(n/N) ) + (0.08 * COS(2.0 * n/N) ).
		audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
	}

	void BandBuffer() {
		for (int i = 0; i < 8; ++i) {

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

	void MakeFrequencyBands() {
		/*
		 * создаем 8 диапазонов частот
		 * 22050 / 512 = 43 herts / sample
		 * 
		 * 20 - 60 herts
		 * 60 - 250 herts
		 * 250 - 500 herts
		 * 500 - 2000 herts
		 * 2000 - 4000 herts
		 * 4000 - 6000 herts
		 * 6000 - 20000 herts
		 * 
		 * 0: 2 = 86 herts
		 * 1: 4 = 172 herts (87 - 258)
		 * 2: 8 = 344 herts (259 - 602)
		 * 3: 16 = 688 herts (603 - 1290)
		 * 4: 32 = 1376 herts (1291 - 2666)
		 * 5: 64 = 2752 herts (2667 - 5418)
		 * 6: 128 = 5504 herts (5419 - 10922)
		 * 7: 256 = 11008 herts (10923 - 21930)
		 *
		 * 510 + 2 будут помещены в 7
		 */

		// номер сэмпла
		int count = 0;

		// для каждого диапазона вычисляем (average * 10)
		for (int i = 0; i < 8; i++) {

			float average = 0;

			// вычисляем кол-во сэмлов в диапазоне
			int sampleCount = (int)Mathf.Pow(2, i) * 2;

			// если последний диапазон, то добавляем оставшиеся 2
			if (i == 7) sampleCount += 2;

			// вычисляем average
			for (int j = 0; j < sampleCount; j++) {
				average += samples[count] * (count + 1);
				count++;
			}

			average /= count;

			// сохраняем average увеличивая в 10 раз, т.к. число маленькое
			freqBand[i] = average * 10;
		}
	}
}
