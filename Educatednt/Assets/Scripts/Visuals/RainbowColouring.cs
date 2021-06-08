using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RainbowColouring : MonoBehaviour {
    public enum ColourType
    {
        BASE_MAP,
        EMISSIVE_COLOUR,
        BOTH
    }


    [SerializeField]
    private ColourType _colourType = ColourType.EMISSIVE_COLOUR;


    [SerializeField, Range( -30f, 30f )]
    private float _hueShift = -2f;


    private Material _material = null;


    private void Start() {
        LoadComponents();
    }

    
    private void LoadComponents() {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Debug.Assert( null != renderer, $"MeshRenderer missing on {name}. " );

        if ( renderer ) {
            _material = new Material( renderer.material );
            renderer.material = _material;

            switch ( _colourType ) {
                case ColourType.EMISSIVE_COLOUR:
                    _material.EnableKeyword( "_EmissiveColor" );
                    break;
                case ColourType.BOTH:
                    _material.EnableKeyword( "_EmissiveColor" );
                    break;
            }
        }
    }


    private void Update() {
        if ( _material ) {
            switch ( _colourType ) {
                case ColourType.BASE_MAP:
                    Color colour = _material.color;
                    Color appliedColour = UpdateHue( colour );
                    _material.color = appliedColour;
                    break;
                case ColourType.EMISSIVE_COLOUR:
                    colour = _material.GetColor( "_EmissiveColor" );
                    appliedColour = UpdateHue( colour );
                    _material.SetColor( "_EmissiveColor", appliedColour );
                    break;
                case ColourType.BOTH:
                    colour = _material.color;
                    appliedColour = UpdateHue( colour );
                    _material.color = appliedColour;

                    colour = _material.GetColor( "_EmissiveColor" );
                    appliedColour = UpdateHue( colour );
                    _material.SetColor( "_EmissiveColor", appliedColour );
                    break;
            }
        }
    }


    private Color UpdateHue( Color colour ) {
        Color.RGBToHSV( colour, out float h, out float s, out float v );

        h += _hueShift / 360f;
        if ( h < 0 ) h += 1;
        else if ( h > 1 ) h -= 1;

        Color appliedColour = Color.HSVToRGB( h, s, v );
        return appliedColour;
    }
}
