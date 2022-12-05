var the_timeout;
var hasRunned = false;

function begincount(id,ids) {
  the_timeout = setTimeout("CountWords('"+id+"','"+ids+"');",500);
  hasRunned = true;
}

function endcount(){
	if(hasRunned){
		clearTimeout(the_timeout);
	}
}

function CountWords(id1,ids1){
     var sapo_text 		= document.getElementById(id1);
	 var monField 		= document.getElementById(ids1);
     var leftChars 		= getLeftChars(sapo_text);

     if (leftChars >= 0) {	 	
 	   monField.innerHTML = leftChars;
 	   return true;
     }
	 else{
     	monField.innerHTML = "0";
     	//window.alert("please check message length!");
     	var len = sapo_text.value.length + leftChars; 	
	 	sapo_text.value = sapo_text.value.substring(0, len);
 		leftChars = getLeftChars(sapo_text);
     	if (leftChars >= 0) {	 	
 	    	monField.value=leftChars;
		}
        return false;    	
     }	
}
 
function getLeftChars(varField){
    var i = 0;
    var counter = 0;
    var max_chars = 500;    
         
    /*for (i = 0; i< varField.value.length; i++){    	
    	if (varField.value.charCodeAt(i) > 127 || varField.value.charCodeAt(i) == 94){
    		max_chars = 70;
		}
 	}*/
	
    var leftchars = max_chars - varField.value.length;           
    return (leftchars);
}
 

 