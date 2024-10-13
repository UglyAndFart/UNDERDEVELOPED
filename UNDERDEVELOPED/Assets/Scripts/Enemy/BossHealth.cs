using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField]
    private Slider _hpBar;
    [SerializeField]
    private TextMeshProUGUI _bossName;
    [SerializeField]
    private Enemy _enemy;
    public static BossHealth _instance; 

    private void Awake()
    {
        if (_instance != null & _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    private void Update()
    {
        if (_enemy == null)
        {
            return;
        }

        _hpBar.value = _enemy.GetHealth();

        if (_enemy.GetHealth() <= 0)
        {
            StartCoroutine(DisableHPBar());
        }
    }

    public void SetBoss(Enemy enemy)
    {
        _enemy = enemy;
        _hpBar.maxValue = _enemy.GetHealth();
        _bossName.text = _enemy.GetName();
    }

    // private void SetHPName()
    // {
    //     _hpBar.maxValue = _enemy.GetHealth();
    //     _bossName.text = _enemy.GetName();
    // }

    private IEnumerator DisableHPBar()
    {
        yield return new WaitForSeconds(2);
        HUDManager._instance.CloseBossHealthbar();
        enabled = false;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
