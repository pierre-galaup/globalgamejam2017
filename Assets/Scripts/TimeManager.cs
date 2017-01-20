using System.Collections;
using System.Globalization;
using UnityEngine;

namespace GameTime
{
    public class TimeManager : MonoBehaviour
    {
        private float _timer = 0f;

        private bool _gameIsPaused = true;
        private bool _timerIsOn = false;

        private Coroutine _coroutine;

        public void StartTimer(float time)
        {
            _timer = time;
            _gameIsPaused = false;
            _timerIsOn = true;
            GameManager.Instance.GuiManager.UpdateTime(GetCurrentTime());
            _coroutine = StartCoroutine(NextTime());
        }

        public void StopTimer()
        {
            _timerIsOn = false;
            _gameIsPaused = true;
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        public void PauseTimer()
        {
            _gameIsPaused = true;
        }

        public void UnPauseTimer()
        {
            _gameIsPaused = false;
        }

        public float GetTimer()
        {
            return _timer;
        }

        public string GetCurrentTime()
        {
            string minutes = ((int)(_timer / 60)).ToString(CultureInfo.InvariantCulture);
            string seconds = (_timer % 60).ToString(CultureInfo.InvariantCulture);

            return minutes.PadLeft(2, '0') + ":" + seconds.PadLeft(2, '0');
        }

        private IEnumerator NextTime()
        {
            while (_timerIsOn && _timer > 0f)
            {
                yield return new WaitForSeconds(1f);
                _gameIsPaused = Time.timeScale == 0;
                if (!_gameIsPaused)
                {
                    _timer -= 1f;
                    GameManager.Instance.GuiManager.UpdateTime(GetCurrentTime());
                }
            }
            GameManager.Instance.EndTimer();
        }
    }
}