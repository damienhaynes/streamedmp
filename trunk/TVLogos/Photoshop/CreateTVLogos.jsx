//[Header]
// Christophe Aeschlimann
// Comments : chli@romandie.com 
//
// Created for Media Portal Community
// Visit http://mediaportal.sourceforge.net
//

// *******************
// * Quick Use Guide *
// *******************

// Prerequisite :
// -------------
// Logos.psd
// Adobe Photoshop CS or 7 with scripting plugin :
// http://www.adobe.com/support/downloads/detail.jsp?ftpID=1536  
   

// Open logos.psd file with Photoshop (tested with CS2 ,CS3 version)
// Modify the background or keep it invisible for transparent background
// Modify the shape color or shape
// Add any logo you want in the "Logos" groups
// For each logo in the "Logos" group a new png file will be created in the 
//	 directory where the StreamedMP_TVLogos.psd has been opened.
// WARNING : The files created use the layers names for their filenames 
// 	so avoid forbidden characters like '/' and so on or it will crash when saving
// Once everything is done go to File/Scripts/Browse... and open this file.
// Files created

// This script can be freely distributed / modified as long as this header is unchanged.

// Modified for "German TV-Logos / Deutsche Senderlogos" by MasterMarc
// Modified for StreamedMP  by Losttown
// [/Header]

// [Code]
displayDialogs = DialogModes.NO;

saveOptions = new PNGSaveOptions();

//check that a document is opened and that it has been saved
if ((documents.length != 0) && (activeDocument.saved))
{
	
	//get active document
	var AD = activeDocument;
	//get current paths
	var currentFolder = AD.path;
	//create a new subfolder



	//get all the layers from the group of layers named "Backgrounds"
	var myBackGrounds = AD.layerSets.getByName("Backgrounds");
	
	//first hide all logos
	for(j=0;j<myBackGrounds.layers.length;j++)
	{
		//get current logo's layer
		var BL = myBackGrounds.layers[j];
		//hide it
		BL.visible = 0;
	}

	//for each layers in the logos group
	for(j=0;j<myBackGrounds.layers.length;j++)
	{
		//get current layer
		var BL = myBackGrounds.layers[j];
		//if it's not the first time
		if (j!=0)
		{
			//hide the old one
			myBackGrounds.layers[j-1].visible = 0; 	
		}
		//make the current layer visible
		BL.visible = 1;	
		
		
		var tempFolder = new Folder (currentFolder + "/" + myBackGrounds.layers[j].name);
		tempFolder.create();
		
		//get all the layers from the group of layers named "Logos"
		var myLogos = AD.layerSets.getByName("Logos");
		
		//first hide all tv logos
		for(i=0;i<myLogos.layers.length;i++)
		{
			//get current logo's layer
			var CL = myLogos.layers[i];
			//hide it
			CL.visible = 0;
		}
		
		//for each layers in the logos group
		for(i=0;i<myLogos.layers.length;i++)
		{
			//get current layer
			var CL = myLogos.layers[i];
			//if it's not the first time
			if (i!=0)
			{
				//hide the old one
				myLogos.layers[i-1].visible = 0; 	
			}
			//make the current layer visible
			CL.visible = 1;		
			//this line crash if the layer's name contains a /
			newFile = new File(tempFolder+"/"+myLogos.layers[i].name);
			AD.saveAs (newFile,saveOptions, true, Extension.LOWERCASE);
		}

		//hide tv logos logos
		for(i=0;i<myLogos.layers.length;i++)
		{
			//get current logo's layer
			var CL = myLogos.layers[i];
			//hide it
			CL.visible = 0;
		}

	}

	alert("PNG files saved !");
}
else
{
	alert("Please first save the document or open logos.psd!");
}
// [/Code]