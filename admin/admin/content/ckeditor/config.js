/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.contentsCss = '../../styles/bootstrap.min.css';
    config.toolbarGroups = [
            { name: 'styles', groups: ['styles'] },
            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
            { name: 'clipboard', groups: ['clipboard', 'undo'] },
            { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
            { name: 'forms', groups: ['forms'] },
            { name: 'colors', groups: ['colors'] },
            { name: 'links', groups: ['links'] },
            { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
            { name: 'insert', groups: ['insert'] },
            { name: 'tools', groups: ['tools'] },
            { name: 'others', groups: ['others'] },
            { name: 'about', groups: ['about'] },
            { name: 'document', groups: ['mode', 'doctools', 'document'] }

    ];

    config.removeButtons = 'Cut,Copy,Paste,PasteText,PasteFromWord,Find';
    //config.removeButtons = 'Save,NewPage,Preview,Print,Cut,Copy,Paste,PasteText,PasteFromWord,Find,Replace,SelectAll,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Language,About';



    config.filebrowserBrowseUrl = '/content/ckfinder/ckfinder.html?type=Files';
    config.filebrowserImageBrowseUrl = '/content/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/content/ckfinder/ckfinder.html?Type=Flash';

    config.filebrowserUploadUrl = '/content/ckfinder/core/connector/aspx/connector.aspx?type=Files';
    config.filebrowserImageUploadUrl = '/content/ckfinder/core/connector/aspx/connector.aspx?type=Images';
    config.filebrowserFlashUploadUrl = '/content/ckfinder/core/connector/aspx/connector.aspx?type=Flash';
};
