// JScript File

function selecte_all_checkbox(form_name,chkID){
	var frm_name = eval("document."+form_name);
	for (var i = 0; i < frm_name.elements.length; i++) {
		if (frm_name.elements[i].type == "checkbox") {
			frm_name.elements[i].checked = (frm_name.elements[i].checked==true)?false:true;
			
			try{
				document.getElementById(chkID).checked = (frm_name.elements[i].checked==true)?true:false;
			}
			catch (e){
							//Do nothing?
			}
		}
	}
}

function CheckAll(obj, itemChkID) {
    for (var i = 0; i < obj.form.elements.length; i++) {
        if (obj.form.elements[i].name.indexOf(itemChkID) != -1 && obj.form.elements[i].type == 'checkbox' && obj.form.elements[i].name != obj.name) {
            if (obj.checked) {
                obj.form.elements[i].checked = true;
            }
            else {
                obj.form.elements[i].checked = false;
            }
        }
    }
}
function CheckOther(obj) {
    document.getElementById(headerid).checked = false;
}
