var fadeAlpha = .0;                               
var fadeTo = .10;                                    
var fadeRate = .22;                                 
static var cursorIsFading = true;                  
private var fadeValue = fadeAlpha;
private var aux = 1;

function Start () 
{

}

function Update () 
{
	if (cursorIsFading) 
	{
        guiElement.material.color.a = fadeValue;
        fadeValue += fadeRate * Time.deltaTime;
       
        if (fadeValue > fadeTo)
        {
            fadeValue = fadeTo;
            cursorIsFading = false;
            aux = 2;
            
			if (aux == 2)
			{
				fadeValue = 0;
			}            
            
            
        }
        
   
		
	}
}