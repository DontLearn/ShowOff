using UnityEngine;
using UnityEngine.UI;
using Player;


public class Debugger : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack _attack = null;


    private Text _textBlock = null;
    private float _textTime = 0;



    private void Start() {
        _textBlock = GetComponent<Text>();
    }


    private void Update() {
        _textTime -= Time.deltaTime;
        if ( _textTime <= 0 ) {
            _textBlock.text = "";
        }

        if ( null != _attack ) {
            if ( _attack.pressed ) {
                _textBlock.text += $"Attack pressed.\r\n";
                _attack.pressed = false;
                _textTime = 5f;
            }
            if ( _attack.struck ) {
                _textBlock.text += "An enemy was hit.\r\n";
                _attack.struck = false;
                _textTime = 5f;
            }
        }
    }
}
