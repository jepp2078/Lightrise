

class ParticleWindow extends EditorWindow 
{
    var mScale = 1.0;
    
    @MenuItem ("Custom/particle scale")
    
    static function Init()
    {
        var window : ParticleWindow = EditorWindow.GetWindow( ParticleWindow );
    }
    
    function OnGUI()
    {
        var go = Selection.GetFiltered(typeof(GameObject), SelectionMode.TopLevel );
		
		var name : String;
		
		if ( go.Length )
			name = go[ 0 ].name;
		else
			name = "select go";
			
        GUILayout.Label ("Object name : " + name, EditorStyles.boldLabel);

        mScale = EditorGUILayout.Slider ("scale : ", mScale, 0.01f, 5.0f);        
        
		if ( GUI.Button( new Rect( 50, 50, 100, 40 ), "set value" ) )
		{
			var ok : boolean = false;
			
			for ( var child : Transform in go[ 0 ].transform )
			{
				if ( child.gameObject.GetComponent.<ParticleEmitter>() )
				{
					child.gameObject.GetComponent.<ParticleEmitter>().minSize *= mScale;
					child.gameObject.GetComponent.<ParticleEmitter>().maxSize *= mScale;
					child.gameObject.GetComponent.<ParticleEmitter>().worldVelocity *= mScale;
					child.gameObject.GetComponent.<ParticleEmitter>().localVelocity *= mScale;
					child.gameObject.GetComponent.<ParticleEmitter>().rndVelocity *= mScale;
					child.gameObject.GetComponent.<ParticleEmitter>().angularVelocity *= mScale;
					child.gameObject.GetComponent.<ParticleEmitter>().rndAngularVelocity *= mScale;
					
					
					ok = true;				
				}
			}
			
			if ( ok )
				go[ 0 ].transform.localScale *= mScale;
				
			if ( ok )
				Debug.Log( "ok!" );
			else
				Debug.Log( "something is wrong!" );
			

		
		}
    }
    
}


