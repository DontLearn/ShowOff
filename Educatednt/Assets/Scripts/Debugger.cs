using UnityEngine;
using UnityEngine.UI;
using Combat;
using Player;


public class Debugger : MonoBehaviour
{
    [SerializeField]
    //private PlayerAttack _attack = null;
    //private Ingredient _ingredient = null;
    //private Slot _slot = null;
    //private Inventory _inventory = null;
    private bool _shown = false;


    private Text _textBlock = null;
    private float _textTime = 0;
    private float _lifeTime = 3.33f;



    private void Start() {
        _textBlock = GetComponent<Text>();
    }


    private void Update() {
        _textTime -= Time.deltaTime;
        if ( _textTime <= 0 ) {
            _textBlock.text = "";
        }


        /*if ( null != _inventory ) {
            if ( !_shown && _inventory.HOUSTON > 0 ) {
                _shown = true;

                if ( _inventory.HOUSTON == 1 ) {
                    _textBlock.text += $"SOMETHING IS WRONG.\r\n";
                }
                else {
                    _textBlock.text += $"EVERYTHING OK.\r\n";
                }
                _textTime = _lifeTime * 4f;
            }
        }*/

        /*if ( null != _slot && !_shown ) {
            if ( _slot.pickupSprites.Length > 0 ) {
                _shown = true;
                for ( int i = 0; i < _slot.pickupSprites.Length; ++i ) {
                    _textBlock.text += $"Slot sprite {i}: {_slot.pickupSprites[ i ].name}.\r\n";
                }
                _textBlock.text += $"Slot number: {_slot._slotNumber}.\r\n";
                _textTime = _lifeTime * 4f;
            }
        }*/

        /*if ( null != _ingredient ) {
            _textBlock.text += $"Ingredient type: {_ingredient.Value.ToString()}.\r\n";
            _textTime = _lifeTime;
        }*/

        /*if ( null != _attack ) {
            if ( _attack.pressed ) {
                _textBlock.text += $"Attack pressed.\r\n";
                _textBlock.text += $"Damage: {_attack.Damage}.\r\n";
                _attack.pressed = false;
                _textTime = _lifeTime;
            }
            if ( _attack.struck ) {
                _textBlock.text += "An enemy was hit.\r\n";
                _attack.struck = false;
                _textTime = _lifeTime;
            }
        }*/
    }
}
