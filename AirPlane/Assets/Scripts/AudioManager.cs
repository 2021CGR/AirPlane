using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 전역에서 사운드를 관리하는 오디오 매니저
/// 하나의 인스턴스로 모든 효과음을 재생함
/// </summary>
public class AudioManager: MonoBehaviour
{
    public static AudioManager Instance; // 싱글톤으로 사용

    [System.Serializable]
    public class Sound
    {
        public string name;              // 사운드 이름 (예: "Heal", "Shoot")
        public AudioClip clip;           // 사운드 파일
        public float volume = 1.0f;      // 개별 볼륨 설정
    }

    [Header("🔊 사운드 목록")]
    public List<Sound> sounds;           // 인스펙터에서 설정할 사운드 리스트

    private Dictionary<string, Sound> soundDict;  // 빠른 검색을 위한 이름 → 사운드 딕셔너리

    private void Awake()
    {
        // 싱글톤 패턴: 오직 하나만 존재하게 하기
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환해도 살아있음
        }
        else
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        // 딕셔너리 초기화
        soundDict = new Dictionary<string, Sound>();
        foreach (var sound in sounds)
        {
            if (!soundDict.ContainsKey(sound.name))
                soundDict.Add(sound.name, sound);
        }
    }

    /// <summary>
    /// 이름을 기반으로 사운드를 재생하는 함수
    /// </summary>
    public void Play(string name)
    {
        if (soundDict.ContainsKey(name))
        {
            Sound s = soundDict[name];
            AudioSource.PlayClipAtPoint(s.clip, Camera.main.transform.position, s.volume); // 현재 카메라 위치에서 재생
        }
        else
        {
            Debug.LogWarning("❌ 사운드 이름이 존재하지 않음: " + name);
        }
    }
}
