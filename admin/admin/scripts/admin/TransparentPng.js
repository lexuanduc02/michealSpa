if (navigator.platform == "Win32" && navigator.appName == "Microsoft Internet Explorer" && window.attachEvent && navigator.userAgent.indexOf("Opera")==-1)
{
	document.writeln('<style type="text/css">img { visibility:hidden; } </style>');
	window.attachEvent("onload", LoadPng);
}

function LoadPng()
{
	var rslt = navigator.appVersion.match(/MSIE (\d+\.\d+)/, '');
	var itsAllGood = (rslt != null && Number(rslt[1]) >= 5.5);

	for (var i = document.images.length - 1, img = null; (img = document.images[i]); i--)
	{
		if (itsAllGood && img.src.match(/\.png$/i) != null)
		{
			var src = img.src;
			img.style.width = img.width + "px";
			img.style.height = img.height + "px";
			img.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + src + "', sizingMethod='scale')"
			img.src = "../admin/icon/null.gif";
		}
		img.style.visibility = "visible";
	}
}