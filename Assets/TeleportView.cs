using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class TeleportView : MonoBehaviour
{
    private static TeleportView instance;


    [Header("Post-Process Volume")] public PostProcessVolume volume;

    private LensDistortion distortion;
    private ChromaticAberration chromatic;
    private Vignette vignette;
    private Bloom bloom;

    public bool isTransitioning = false;
    [SerializeField] private float duration = 2;
    [SerializeField] private float distortionval = -0.9f;
    [SerializeField] private float chromaticval = 0.8f;
    [SerializeField] private float vignetteval = 0.8f;
    [SerializeField] private float bloomval = 10f;

    public static TeleportView Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TeleportView>();
            }

            return instance;
        }
    }

    void Start()
    {
        if (volume == null)
            volume = FindObjectOfType<PostProcessVolume>();

        if (volume == null)
        {
            Debug.LogError("❌ PostProcessVolume не найден!");
            return;
        }

        // Получаем все эффекты
        volume.profile.TryGetSettings(out distortion);
        volume.profile.TryGetSettings(out chromatic);
        volume.profile.TryGetSettings(out vignette);
        volume.profile.TryGetSettings(out bloom);

        if (distortion == null)
        {
            Debug.LogError("❌ Добавь Lens Distortion в профиль!");
            return;
        }

        // Включаем всё
        distortion.enabled.Override(true);
        distortion.intensity.Override(0f);
        //  distortion.scale.Override(-20f); 
        if (chromatic != null)
        {
            chromatic.enabled.Override(true);
            chromatic.intensity.Override(0f);
        }

        if (vignette != null)
        {
            vignette.enabled.Override(true);
            vignette.intensity.Override(0f);
        }

        if (bloom != null)
        {
            bloom.enabled.Override(true);
            bloom.intensity.Override(0f);
        }

        Debug.Log("✅ Готово! Нажми SPACE для запуска.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTransitioning)
        {
            StartCoroutine(FullTransition());
        }
    }

    public IEnumerator FullTransition()
    {
        if (isTransitioning) yield return null;
        isTransitioning = true;
        Debug.Log("🚀 ЗАПУСК ПОЛНОГО ПЕРЕХОДА!");
        float elapsed = 0f;

        // ФАЗА 1: Нарастание эффектов
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float smoothT = t * t; // Плавное ускорение

            // Главное искажение
            distortion.intensity.value = Mathf.Lerp(0f, distortionval, smoothT);

            // Цветное разложение
            if (chromatic != null)
                chromatic.intensity.value = Mathf.Lerp(0f, chromaticval, smoothT);

            // Затемнение краев
            if (vignette != null)
                vignette.intensity.value = Mathf.Lerp(0f, vignetteval, smoothT);

            // Вспышка в конце фазы
            if (bloom != null)
            {
                float bloomT = Mathf.Max(0, (t - 0.6f) * 2.5f);
                bloom.intensity.value = Mathf.Lerp(0f, bloomval, bloomT);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 💥 МОМЕНТ ПЕРЕХОДА МЕЖДУ МИРАМИ
        Debug.Log("🌍 СМЕНА МИРА!");
        // Здесь ты меняешь сцену или перемещаешь игрока
        yield return new WaitForSeconds(0.3f);

        // ФАЗА 2: Возврат в норму
        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float smoothT = 1 - (1 - t) * (1 - t); // Плавное замедление

            distortion.intensity.value = Mathf.Lerp(distortionval, 0f, smoothT);

            if (chromatic != null)
                chromatic.intensity.value = Mathf.Lerp(chromaticval, 0f, smoothT);

            if (vignette != null)
                vignette.intensity.value = Mathf.Lerp(vignetteval, 0f, smoothT);

            if (bloom != null)
                bloom.intensity.value = Mathf.Lerp(bloomval, 0f, smoothT);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Финальная очистка
        distortion.intensity.value = 0f;
        if (chromatic != null) chromatic.intensity.value = 0f;
        if (vignette != null) vignette.intensity.value = 0f;
        if (bloom != null) bloom.intensity.value = 0f;

        isTransitioning = false;
        Debug.Log("✅ ПЕРЕХОД ЗАВЕРШЕН!");
    }
}