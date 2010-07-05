using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Security;
using System.Diagnostics;

namespace StreamedMPEditor
{
  public partial class streamedMpEditor
  {
    #region Main Generation Function

    private void generateXML(menuType direction)
    {
      string menuPos;
      string skeletonFile;

      int onleft = 0;
      int onright = 0;

      writeMenuProfile(direction);
      bgItems.Clear();

      if (direction == menuType.horizontal)
      {
        menuPos = "menuYPos:" + txtMenuPos.Text;
        skeletonFile = "StreamedMPEditor.xmlFiles.HBasicHomeSkeleton.xml";
      }
      else
      {
        menuPos = "menuXPos:" + txtMenuPos.Text;
        skeletonFile = "StreamedMPEditor.xmlFiles.VBasicHomeSkeleton.xml";
      }
      if (getInfoServiceVersion().CompareTo("1.6.0.0") >= 0)
        infoServiceDayProperty = "forecast";
      else
        infoServiceDayProperty = "day";

      Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(skeletonFile);
      StreamReader reader = new StreamReader(stream);
      xml = reader.ReadToEnd();

      const string quote = "\"";
      string acceleration = tbAcceleration.Text;
      string duration = tbDuration.Text;

      string randomFanartGames = randomFanart.fanartGames ? "Yes" : "No";
      string randomFanartMovies = randomFanart.fanartMovies ? "Yes" : "No";
      string randomFanartMovingPictures = randomFanart.fanartMovingPictures ? "Yes" : "No";
      string randomFanartMusic = randomFanart.fanartMusic ? "Yes" : "No";
      string randomFanartPictures = randomFanart.fanartPictures ? "Yes" : "No";
      string randomFanartPlugins = randomFanart.fanartPlugins ? "Yes" : "No";
      string randomFanartTv = randomFanart.fanartTv ? "Yes" : "No";
      string randomFanartTVSeries = randomFanart.fanartTVSeries ? "Yes" : "No";
      string randomScoreCenterFanart = randomFanart.fanartScoreCenter ? "Yes" : "No";


      xml = xml.Replace("<!-- BEGIN GENERATED DEFINITIONS -->"
                      , "<define>#menuitemFocus:" + focusAlpha.Text + txtFocusColour.Text + "</define>\n"
                      + "\t<define>#menuitemNoFocus:" + noFocusAlpha.Text + txtNoFocusColour.Text + "</define>\n"
                      + "\t<define>#labelFont:" + cboLabelFont.Text + "</define>\n"
                      + "\t<define>#selectedFont:" + cboSelectedFont.Text + "</define>\n"
                      + "\t<define>#" + menuPos + "</define>\n"
                      + "\t<define>#useRandomGamesFanart:" + randomFanartGames + "</define>\n"
                      + "\t<define>#useRandomTVSeriesFanart:" + randomFanartTVSeries + "</define>\n"
                      + "\t<define>#useRandomPluginsFanart:" + randomFanartPlugins + "</define>\n"
                      + "\t<define>#useRandomMovingPicturesFanart:" + randomFanartMovingPictures + "</define>\n"
                      + "\t<define>#useRandomMusicFanart:" + randomFanartMusic + "</define>\n"
                      + "\t<define>#useRandomPicturesFanart:" + randomFanartPictures + "</define>\n"
                      + "\t<define>#useRandomTVFanart:" + randomFanartTv + "</define>\n"
                      + "\t<define>#useRandomMoviesFanart:" + randomFanartMovies + "</define>\n"
                      + "\t<define>#useRandomScoreCenterFanart:" + randomScoreCenterFanart + "</define>\n");



      StringBuilder rawXML = new StringBuilder();
      foreach (menuItem menItem in menuItems)
      {

        fillBackgroundItem(menItem);

        if (menItem.isDefault == true)
          xml = xml.Replace("<!-- BEGIN GENERATED DEFAULTCONTROL CODE-->", "<defaultcontrol>" + (menItem.id + 900).ToString() + "</defaultcontrol>");

        rawXML.AppendLine("\n<control>");
        rawXML.AppendLine("\t<description>Dummy label indicating " + menItem.name + " visibility</description>");
        rawXML.AppendLine("\t<id>" + menItem.id.ToString() + "</id>");
        rawXML.AppendLine("\t<type>label</type>");
        rawXML.AppendLine("\t<posX>100</posX>");
        rawXML.AppendLine("\t<posY>-100</posY>");
        rawXML.AppendLine("\t<width>500</width>");
        rawXML.AppendLine("\t<height>0</height>");
        rawXML.AppendLine("\t<label>Movies</label>");
        rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")|Control.HasFocus(" + (menItem.id + 900).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")</visible>");
        rawXML.AppendLine("\t</control>");


        // Check if this menu item is TVSeries or MovingPictures and store the menu ID for use
        // with the InfoService 3 last added items function if a match
        if (menItem.hyperlink == tvseriesSkinID)
          basicHomeValues.tvseriesControl = menItem.id;

        if (menItem.hyperlink == movingPicturesSkinID)
          basicHomeValues.movingPicturesControl = menItem.id;

        for (int i = 0; i < 14; i++)
        {
          if (direction == menuType.horizontal)
          {
            switch (i)
            {
              case 0:

                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>button</type>");
                rawXML.AppendLine("\t<id>" + (menItem.id + 700).ToString() + "</id>");
                rawXML.AppendLine("\t<posX>0</posX>");
                rawXML.AppendLine("\t<posY>-30</posY>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");
                if (menItem.isDefault)
                {
                  rawXML.AppendLine("\t<width>1280</width>");
                  rawXML.AppendLine("\t<height>720</height>");
                }
                else
                {
                  rawXML.AppendLine("\t<width>320</width>");
                  rawXML.AppendLine("\t<height>72</height>");
                }
                rawXML.AppendLine("\t<textureFocus>-</textureFocus>");
                rawXML.AppendLine("\t<textureNoFocus>-</textureNoFocus>");

                if (menItem.name.ToLower() == "shutdown")
                  rawXML.AppendLine("\t<action>99</action>");
                else if (menItem.name.ToLower() == "exit")
                  rawXML.AppendLine("\t<action>97</action>");
                else
                  rawXML.AppendLine("\t<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");


                rawXML.AppendLine("\t<hover>-</hover>");

                if (menuItems.IndexOf(menItem) == 0)
                  onleft = menuItems[menuItems.Count - 1].id;
                else
                  onleft = (menItem.id - 1);
                if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                  onright = menuItems[0].id;
                else
                  onright = (menItem.id + 1);
                rawXML.AppendLine("\t<onleft>" + (onleft + 800).ToString() + "</onleft>");
                rawXML.AppendLine("\t<onright>" + (onright + 700).ToString() + "</onright>");
                rawXML.AppendLine("\t<onup>" + (menItem.id + 600).ToString() + "01</onup>");
                rawXML.AppendLine("\t<visible>control.isvisible(" + (menItem.id + 700).ToString() + ")</visible>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;

              case 1:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>button</type>");
                rawXML.AppendLine("\t<id>" + (menItem.id + 800).ToString() + "</id>");
                rawXML.AppendLine("\t<posX>0</posX>");
                rawXML.AppendLine("\t<posY>-30</posY>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textureFocus>-</textureFocus>");
                rawXML.AppendLine("\t<textureNoFocus>-</textureNoFocus>");

                if (menItem.name.ToLower() == "shutdown")
                  rawXML.AppendLine("\t<action>99</action>");
                else if (menItem.name.ToLower() == "exit")
                  rawXML.AppendLine("\t<action>97</action>");
                else
                  rawXML.AppendLine("\t<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");

                rawXML.AppendLine("\t<hover>-</hover>");

                if (menuItems.IndexOf(menItem) == 0)
                  onleft = menuItems[menuItems.Count - 1].id;
                else
                  onleft = (menItem.id - 1);
                if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                  onright = menuItems[0].id;
                else
                  onright = (menItem.id + 1);
                rawXML.AppendLine("\t<onleft>" + (onleft + 800).ToString() + "</onleft>");
                rawXML.AppendLine("\t<onright>" + (onright + 700).ToString() + "</onright>");
                rawXML.AppendLine("\t<onup>" + (menItem.id + 600).ToString() + "01</onup>");
                //rawXML.AppendLine("\t<ondown>" + (menItem.id + 700).ToString() + "01</ondown>");
                rawXML.AppendLine("\t<visible>control.isvisible(" + (menItem.id + 800).ToString() + ")</visible>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;

              case 2:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>-160</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t<font>#labelFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 1)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 2)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 3)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[2].id + 700).ToString() + ")|control.isvisible(" + (menuItems[2].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|control.isvisible(" + (menItem.id + 101).ToString() + ")</visible>");

                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 3:
                rawXML.AppendLine("\t<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>1120</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t<font>#labelFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 2 == -2)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 2].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 2 == -1)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 698).ToString() + ")|control.isvisible(" + (menItem.id + 98).ToString() + ")</visible>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 4:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>800</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t<font>#labelFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 1 == -1)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 699).ToString() + ")|control.isvisible(" + (menItem.id + 99).ToString() + ")</visible>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 5:
                if (cbDropShadow.Checked)
                {
                  rawXML.AppendLine("<control>");
                  rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                  rawXML.AppendLine("\t<type>label</type>");
                  rawXML.AppendLine("\t<posX>482</posX>");
                  rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 2) + "</posY>");
                  rawXML.AppendLine("\t<width>320</width>");
                  rawXML.AppendLine("\t<height>72</height>");
                  rawXML.AppendLine("\t<textcolor>" + dropShadowColor + "</textcolor>");
                  rawXML.AppendLine("\t<font>#selectedFont</font>");
                  rawXML.AppendLine("\t<align>center</align>");
                  rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")</visible>");

                  rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                  rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                  rawXML.AppendLine("</control>");

                }
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>480</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemFocus</textcolor>");
                rawXML.AppendLine("\t<font>#selectedFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")</visible>");

                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 6:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>160</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t<font>#labelFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|control.isvisible(" + (menItem.id + 101).ToString() + ")</visible>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 7:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>0</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t<font>#labelFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 702).ToString() + ")|control.isvisible(" + (menItem.id + 102).ToString() + ")</visible>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "160,0" + quote + " end=" + quote + "-160,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 8:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>1120</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t<font>#labelFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 3 == -3)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 3].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 3 == -2)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 3 == -1)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + ((menItem.id + 800) - 3).ToString() + ")</visible>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 9:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>800</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t<font>#labelFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 2 == -2)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 2 == -1)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + ((menItem.id + 800) - 2).ToString() + ")</visible>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,0" + quote + " end=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 10:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>800</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t<font>#labelFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 1 == -1)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + ((menItem.id + 800) - 1).ToString() + ")</visible>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 11:
                if (cbDropShadow.Checked)
                {
                  rawXML.AppendLine("<control>");
                  rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                  rawXML.AppendLine("\t<type>label</type>");
                  rawXML.AppendLine("\t<posX>481</posX>");
                  rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 1) + "</posY>");
                  rawXML.AppendLine("\t<width>320</width>");
                  rawXML.AppendLine("\t<height>72</height>");
                  rawXML.AppendLine("\t<textcolor>" + dropShadowColor + "</textcolor>");
                  rawXML.AppendLine("\t<font>#selectedFont</font>");
                  rawXML.AppendLine("\t<align>center</align>");
                  rawXML.AppendLine("\t<label>" + menItem.name + "</label>");
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
                  rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                  rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                  rawXML.AppendLine("</control>");

                }
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>480</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemFocus</textcolor>");
                rawXML.AppendLine("\t<font>#selectedFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");
                rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 12:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>160</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t<font>#labelFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + ((menItem.id + 800) + 1).ToString() + ")</visible>");

                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 13:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t<type>label</type>");
                rawXML.AppendLine("\t<posX>0</posX>");
                rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("\t<width>320</width>");
                rawXML.AppendLine("\t<height>72</height>");
                rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t<font>#labelFont</font>");
                rawXML.AppendLine("\t<align>center</align>");
                rawXML.AppendLine("\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[1].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t<visible>Control.HasFocus(" + ((menItem.id + 800) + 2).ToString() + ")</visible>");

                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-480,0" + quote + " end=" + quote + "-160,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
            }

          }
          else if (direction == menuType.vertical)
          {
            switch (i)
            {
              case 0:

                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>button</type>");
                rawXML.AppendLine("\t\t\t<id>" + (menItem.id + 700).ToString() + "</id>");
                rawXML.AppendLine("\t\t\t<posX>0</posX>");
                rawXML.AppendLine("\t\t\t<posY>-30</posY>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");
                if (menItem.isDefault)
                {
                  rawXML.AppendLine("\t<width>1280</width>");
                  rawXML.AppendLine("\t<height>720</height>");
                }
                else
                {
                  rawXML.AppendLine("\t<width>320</width>");
                  rawXML.AppendLine("\t<height>72</height>");
                }
                rawXML.AppendLine("\t\t\t<textureFocus>-</textureFocus>");
                rawXML.AppendLine("\t\t\t<textureNoFocus>-</textureNoFocus>");
                rawXML.AppendLine("\t\t\t<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");
                rawXML.AppendLine("\t\t\t<hover>-</hover>");

                if (menuItems.IndexOf(menItem) == 0)
                  onleft = menuItems[menuItems.Count - 1].id;
                else
                  onleft = (menItem.id - 1);
                if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                  onright = menuItems[0].id;
                else
                  onright = (menItem.id + 1);
                rawXML.AppendLine("\t\t\t<onright>" + (menItem.id + 600).ToString() + "01</onright>");
                rawXML.AppendLine("\t\t\t<onleft>" + (menItem.id + 600).ToString() + "01</onleft>");
                rawXML.AppendLine("\t\t\t<onup>" + (onleft + 800).ToString() + "</onup>");
                rawXML.AppendLine("\t\t\t<ondown>" + (onright + 700).ToString() + "</ondown>");
                rawXML.AppendLine("\t\t\t<visible>control.isvisible(" + (menItem.id + 700).ToString() + ")</visible>");
                rawXML.AppendLine("\t\t</control>");
                break;

              case 1:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>button</type>");
                rawXML.AppendLine("\t\t\t<id>" + (menItem.id + 800).ToString() + "</id>");
                rawXML.AppendLine("\t\t\t<posX>0</posX>");
                rawXML.AppendLine("\t\t\t<posY>-30</posY>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");
                rawXML.AppendLine("\t\t\t<width>320</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textureFocus>-</textureFocus>");
                rawXML.AppendLine("\t\t\t<textureNoFocus>-</textureNoFocus>");
                rawXML.AppendLine("\t\t\t<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");
                rawXML.AppendLine("\t\t\t<hover>-</hover>");

                if (menuItems.IndexOf(menItem) == 0)
                  onleft = menuItems[menuItems.Count - 1].id;
                else
                  onleft = (menItem.id - 1);
                if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                  onright = menuItems[0].id;
                else
                  onright = (menItem.id + 1);
                rawXML.AppendLine("\t\t\t<onright>" + (menItem.id + 600).ToString() + "01</onright>");
                rawXML.AppendLine("\t\t\t<onleft>" + (menItem.id + 600).ToString() + "01</onleft>");
                rawXML.AppendLine("\t\t\t<onup>" + (onleft + 800).ToString() + "</onup>");
                rawXML.AppendLine("\t\t\t<ondown>" + (onright + 700).ToString() + "</ondown>");
                rawXML.AppendLine("\t\t\t<visible>control.isvisible(" + (menItem.id + 800).ToString() + ")</visible>");
                rawXML.AppendLine("\t\t</control>");
                break;

              case 2:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>-24</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 1)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 2)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 3)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[2].id + 700).ToString() + ")|control.isvisible(" + (menuItems[2].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|control.isvisible(" + (menItem.id + 101).ToString() + ")</visible>");

                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,118" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 3:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>542</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 2 == -2)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 2].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 2 == -1)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 698).ToString() + ")|control.isvisible(" + (menItem.id + 98).ToString() + ")</visible>");

                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 4:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>442</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 1 == -1)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 699).ToString() + ")|control.isvisible(" + (menItem.id + 99).ToString() + ")</visible>");

                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 5:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>342</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#selectedFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");
                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")</visible>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 6:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>242</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|control.isvisible(" + (menItem.id + 101).ToString() + ")</visible>");

                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 7:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>142</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 702).ToString() + ")|control.isvisible(" + (menItem.id + 102).ToString() + ")</visible>");

                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 8:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>-24</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 3 == -3)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 3].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 3 == -2)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 3 == -1)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + ((menItem.id + 800) - 3).ToString() + ")</visible>");

                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-118" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 9:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>542</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 2 == -2)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 2 == -1)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + ((menItem.id + 800) - 2).ToString() + ")</visible>");

                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 10:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>442</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 1 == -1)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + ((menItem.id + 800) - 1).ToString() + ")</visible>");

                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 11:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>342</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#selectedFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");
                rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 12:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>242</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + ((menItem.id + 800) + 1).ToString() + ")</visible>");

                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
              case 13:
                rawXML.AppendLine("\t\t<control>");
                rawXML.AppendLine("\t\t\t<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("\t\t\t<type>label</type>");
                rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("\t\t\t<posY>142</posY>");
                rawXML.AppendLine("\t\t\t<width>500</width>");
                rawXML.AppendLine("\t\t\t<height>72</height>");
                rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
                rawXML.AppendLine("\t\t\t<align>right</align>");
                rawXML.AppendLine("\t\t\t<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[1].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + ((menItem.id + 800) + 2).ToString() + ")</visible>");

                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("\t\t</control>");
                break;
            }
          }
        }
      }
      xml = xml.Replace("<!-- BEGIN GENERATED BUTTON CODE-->", rawXML.ToString());
    }
    #endregion

    #region Generate menu Graphics Horizontal

    private void generateMenuGraphicsH()
    {
      StringBuilder rawXML = new StringBuilder();
      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>Menu Background</description>");
      rawXML.AppendLine("\t<type>image</type>");
      rawXML.AppendLine("\t<id>99003</id>");
      rawXML.AppendLine("\t<posX>0</posX>");
      rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu) + "</posY>");
      rawXML.AppendLine("\t<width>1280</width>");
      rawXML.AppendLine("\t<height>" + basicHomeValues.menuHeight + "</height>");
      rawXML.AppendLine("\t<texture>" + basicHomeValues.mymenu + "</texture>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");
      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("\t<description>Menu Sub Menu</description>");
        rawXML.AppendLine("\t<type>image</type>");
        rawXML.AppendLine("\t<id>99004</id>");
        rawXML.AppendLine("\t<posX>" + basicHomeValues.subMenuXpos + "</posX>");
        rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu).ToString() + "</posY>");
        rawXML.AppendLine("\t<width>" + basicHomeValues.subMenuWidth + "</width>");
        rawXML.AppendLine("\t<height>" + basicHomeValues.subMenuHeight.ToString() + "</height>");
        rawXML.AppendLine("\t<texture>" + basicHomeValues.mymenu_submenu + "</texture>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
        rawXML.AppendLine("</control>");
      }
      xml = xml.Replace("<!-- BEGIN GENERATED MENUGRAPHICS CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate menu Graphics Vertical

    private void generateMenuGraphicsV()
    {
      StringBuilder rawXML = new StringBuilder();
      const string quote = "\"";
      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Menu Background</description>");
      rawXML.AppendLine("\t\t\t<type>image</type>");
      rawXML.AppendLine("\t\t\t<id>1</id>");
      rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) - maxXPosition).ToString() + "</posX>");
      rawXML.AppendLine("\t\t\t<posY>0</posY>");
      rawXML.AppendLine("\t\t\t<width>1280</width>"); rawXML.AppendLine("\t\t\t<height>720</height>");
      rawXML.AppendLine("\t\t\t<texture>basichome.menu.overlay.png</texture>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>!Control.HasFocus(7888)+!Control.HasFocus(7999)+!Control.HasFocus(7777)</visible>");
      rawXML.AppendLine("\t\t</control>");
      if (enableRssfeed.Checked)
      {
        rawXML.AppendLine("\t\t<control>");
        rawXML.AppendLine("\t\t\t<description>Rss Background</description>");
        rawXML.AppendLine("\t\t\t<type>image</type>");
        rawXML.AppendLine("\t\t\t<id>1</id>");
        rawXML.AppendLine("\t\t\t<posX>80</posX>");
        rawXML.AppendLine("\t\t\t<posY>685</posY>");
        rawXML.AppendLine("\t\t\t<width>1300</width>");
        rawXML.AppendLine("\t\t\t<height>50</height>");
        rawXML.AppendLine("\t\t\t<texture>homerss.png</texture>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,100" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(InfoService)+!string.equals(#infoservice.feed.titles,)</visible>");
        rawXML.AppendLine("\t\t</control>");

        rawXML.AppendLine("\t\t<control>");
        rawXML.AppendLine("\t\t\t<description>Weather Background</description>");
        rawXML.AppendLine("\t\t\t<type>image</type>");
        rawXML.AppendLine("\t\t\t<id>1</id>");
        rawXML.AppendLine("\t\t\t<posX>976</posX>");
        rawXML.AppendLine("\t\t\t<posY>-3</posY>");
        rawXML.AppendLine("\t\t\t<width>306</width>");
        rawXML.AppendLine("\t\t\t<height>75</height>");
        rawXML.AppendLine("\t\t\t<texture>homeweatheroverlaybg.png</texture>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(InfoService)+control.hasthumb(43001)</visible>");
        rawXML.AppendLine("\t\t</control>");
      }

      xml = xml.Replace("<!-- BEGIN GENERATED MENUGRAPHICS CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Weather Vertical

    private void generateWeatherV()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Weather image</description>");
      rawXML.AppendLine("\t\t\t<type>image</type>");
      rawXML.AppendLine("\t\t\t<id>43001</id>");
      rawXML.AppendLine("\t\t\t<posX>987</posX>");
      rawXML.AppendLine("\t\t\t<posY>9</posY>");
      rawXML.AppendLine("\t\t\t<height>53</height>");
      rawXML.AppendLine("\t\t\t<width>55</width>");
      rawXML.AppendLine("\t\t\t<centered>yes</centered>");
      rawXML.AppendLine("\t\t\t<texture>#infoservice.weather.today.img.small.fullpath</texture>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("\t\t</control>");

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Temperature</description>");
      rawXML.AppendLine("\t\t\t<type>label</type>");
      rawXML.AppendLine("\t\t\t<id>1</id>");
      rawXML.AppendLine("\t\t\t<width>400</width>");
      rawXML.AppendLine("\t\t\t<height>50</height>");
      rawXML.AppendLine("\t\t\t<align>right</align>");
      rawXML.AppendLine("\t\t\t<posX>1250</posX>");
      rawXML.AppendLine("\t\t\t<posY>22</posY>");
      rawXML.AppendLine("\t\t\t<font>mediastream14c</font>");
      rawXML.AppendLine("\t\t\t<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("\t\t</control>");

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>condition</description>");
      rawXML.AppendLine("\t\t\t<type>textbox</type>");
      rawXML.AppendLine("\t\t\t<id>1</id>");
      rawXML.AppendLine("\t\t\t<width>400</width>");
      rawXML.AppendLine("\t\t\t<height>50</height>");
      rawXML.AppendLine("\t\t\t<align>right</align>");
      rawXML.AppendLine("\t\t\t<posX>1048</posX>");
      rawXML.AppendLine("\t\t\t<posY>15</posY>");
      rawXML.AppendLine("\t\t\t<font>mediastream10tc</font>");
      rawXML.AppendLine("\t\t\t<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("\t\t</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED WEATHER CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate RSS Ticker Horizontal

    private void generateRSSTicker()
    {
      StringBuilder rawXML = new StringBuilder();

      int rssImageXposOffset = 0;
      int rssImageYposOffset = 0;
      if (screenres == screenResolutionType.res1920x1080 && (menuStyle == chosenMenuStyle.horizontalContextStyle) && rssImage == rssImageType.infoserviceImage)
      {
        rssImageXposOffset = 40;
        rssImageYposOffset = 357;
      }
      if (screenres == screenResolutionType.res1920x1080 && (menuStyle == chosenMenuStyle.horizontalStandardStyle) && rssImage == rssImageType.infoserviceImage)
      {
        rssImageXposOffset = 40;
        rssImageYposOffset = 262;
      }
      //rssImageXposOffset = 0;
      //rssImageYposOffset = 0;

      switch (rssImage)
      {
        case rssImageType.infoserviceImage:
          rawXML.AppendLine("\n<control>");
          rawXML.AppendLine("\t<description>RSS Feed image (InfoService)</description>");
          rawXML.AppendLine("\t<type>image</type>");
          rawXML.AppendLine("\t<id>1</id>");
          rawXML.AppendLine("\t<keepaspectratio>yes</keepaspectratio>");
          rawXML.AppendLine("\t<height>26</height>");
          rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssImage + rssImageYposOffset).ToString() + "</posY>");
          rawXML.AppendLine("\t<posX>" + (rssImageXposOffset + 60).ToString() + "</posX>");
          rawXML.AppendLine("\t<texture>#infoservice.feed.img</texture>");
          rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)</visible>");
          rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case rssImageType.skinImage:
          rawXML.AppendLine("\n<control>");
          rawXML.AppendLine("\t<description>RSS Feed image (Default Skin Image)</description>");
          rawXML.AppendLine("\t<type>image</type>");
          rawXML.AppendLine("\t<id>1</id>");
          rawXML.AppendLine("\t<width>24</width>");
          rawXML.AppendLine("\t<height>24</height>");
          rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssImage + 4 + rssImageYposOffset).ToString() + "</posY>");
          rawXML.AppendLine("\t<posX>60</posX>");
          rawXML.AppendLine("\t<texture>InfoService\\defaultFeedRSS.png</texture>");
          rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)</visible>");
          rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
      }

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("\t<description>RSS Items</description>");
      rawXML.AppendLine("\t<type>fadelabel</type>");
      rawXML.AppendLine("\t<id>1</id>");
      rawXML.AppendLine("\t<width>1280</width>");
      rawXML.AppendLine("\t<height>50</height>");
      rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssText).ToString() + "</posY>");
      if (rssImage == rssImageType.skinImage)
        rawXML.AppendLine("\t<posX>90</posX>");
      else if (rssImage == rssImageType.infoserviceImage)
        rawXML.AppendLine("\t<posX>140</posX>");
      else
        rawXML.AppendLine("\t<posX>60</posX>");
      rawXML.AppendLine("\t<font>mediastream12</font>");
      rawXML.AppendLine("\t<textcolor>ff000000</textcolor>");
      rawXML.AppendLine("\t<label>#infoservice.feed.titles</label>");
      if (wrapString.Checked)
      {
        if (useInfoServiceSeparator)
          //rawXML.AppendLine("\t<wrapString> #infoservice.feed.separator </wrapString>");
          rawXML.AppendLine("\t<wrapString> :: </wrapString>");
        else
          rawXML.AppendLine("\t<wrapString> :: </wrapString>");
      }
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED RSS TICKER CODE-->", rawXML.ToString());

    }
    #endregion

    #region Generate RSSTicker Vertical

    private void generateRSSTickerV()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("\t\t\t<description>RSS Items</description>");
      rawXML.AppendLine("\t\t\t<type>fadelabel</type>");
      rawXML.AppendLine("\t\t\t<id>1</id>");
      rawXML.AppendLine("\t\t\t<width>1250</width>");
      rawXML.AppendLine("\t\t\t<height>50</height>");
      rawXML.AppendLine("\t\t\t<posY>695</posY>");
      rawXML.AppendLine("\t\t\t<posX>120</posX>");
      rawXML.AppendLine("\t\t\t<font>mediastream12tc</font>");
      rawXML.AppendLine("\t\t\t<label>#infoservice.feed.titles</label>");
      if (wrapString.Checked)
      {
        if (useInfoServiceSeparator)
          //rawXML.AppendLine("\t<wrapString> #infoservice.feed.separator </wrapString>");
          rawXML.AppendLine("\t<wrapString> :: </wrapString>");
        else
          rawXML.AppendLine("\t\t\t<wrapString> :: </wrapString>");
      }
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,100" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("\t\t</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED RSS TICKER CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Topbar Vertical

    private void generateTopBarV()
    {
      StringBuilder rawXML = new StringBuilder();
      const string quote = "\"";

      foreach (menuItem menItem in menuItems)
      {
        String topMenuId = (menItem.id + 600).ToString();

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Exit Label</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<width>200</width>");
        rawXML.AppendLine("<height>60</height>");
        rawXML.AppendLine("<posY>590</posY>");
        rawXML.AppendLine("<posX>140</posX>");
        rawXML.AppendLine("<align>center</align>");
        rawXML.AppendLine("<font>mediastream10tc</font>");
        rawXML.AppendLine("<label>Exit MediaPortal</label>");
        rawXML.AppendLine("\t\t<visible>Control.HasFocus(" + topMenuId + "01)</visible>");
        rawXML.AppendLine("</control>");
     
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Restart Label</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<width>200</width>");
        rawXML.AppendLine("<height>60</height>");
        rawXML.AppendLine("<posY>590</posY>");
        rawXML.AppendLine("<posX>140</posX>");
        rawXML.AppendLine("<align>center</align>");
        rawXML.AppendLine("<font>mediastream10tc</font>");
        rawXML.AppendLine("<label>Restart MediaPortal</label>");
        rawXML.AppendLine("\t\t<visible>Control.HasFocus(" + topMenuId + "02)</visible>");
        rawXML.AppendLine("</control>");
        
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Shutdown Label</description>");
        rawXML.AppendLine(" <type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<width>200</width>");
        rawXML.AppendLine("<height>60</height>");
        rawXML.AppendLine("<posY>590</posY>");
        rawXML.AppendLine("<posX>140</posX>");
        rawXML.AppendLine("<align>center</align>");
        rawXML.AppendLine("<font>mediastream10tc</font>");
        rawXML.AppendLine("<label>Shutdown Menu</label>");
        rawXML.AppendLine("\t\t<visible>Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("</control>");
        
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Shutdown menu hbar</description>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>1</id>");
        rawXML.AppendLine("<posX>140</posX>");
        rawXML.AppendLine("<posY>630</posY>");
        rawXML.AppendLine("<width>200</width>");
        rawXML.AppendLine("<height>1</height>");
        rawXML.AppendLine("<texture>hbar1white.png</texture>");
        rawXML.AppendLine("\t\t<visible>Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("</control>");





        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<description>Dummy label indicating " + menItem.name + " menu visibility</description>");
        rawXML.AppendLine("\t\t<id>" + (menItem.id + 100).ToString() + "</id>");
        rawXML.AppendLine("\t\t<type>label</type>");
        rawXML.AppendLine("\t\t<posX>100</posX>");
        rawXML.AppendLine("\t\t<posY>-100</posY>");
        rawXML.AppendLine("\t\t<width>500</width>");
        rawXML.AppendLine("\t\t<height>0</height>");
        rawXML.AppendLine("\t\t<label>" + menItem.name + "</label>");
        rawXML.AppendLine("\t\t<visible>Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\n<control>\n\t<description>Topbar buttons " + menItem.name + "</description>");
        rawXML.AppendLine("\t<type>group</type>");
        //rawXML.AppendLine("\t<unfocusedAlpha>200</unfocusedAlpha>");
        rawXML.AppendLine("\t<dimColor>0x60ffffff</dimColor>");
        

        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<description>Exit Button: Menu Item:" + menItem.name + "</description>");
        rawXML.AppendLine("\t\t<type>button</type>");
        rawXML.AppendLine("\t\t<id>" + topMenuId + "01</id>");
        rawXML.AppendLine("\t\t<posX>150</posX>");
        rawXML.AppendLine("\t\t<posY>640</posY>");
        rawXML.AppendLine("\t\t<onleft>" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + "</onleft>");
        rawXML.AppendLine("\t\t<onright>" + topMenuId + "02" + "</onright>");
        rawXML.AppendLine("\t\t\t<onup>" + (menItem.id + 700).ToString() + "</onup>");
        rawXML.AppendLine("\t\t<width>40</width>");
        rawXML.AppendLine("\t\t<height>40</height>");
        rawXML.AppendLine("\t\t<textureNoFocus>exit_button.png</textureNoFocus>");
        rawXML.AppendLine("\t\t<textureFocus>exit_button.png</textureFocus>");
        rawXML.AppendLine("\t\t<action>97</action>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("\t\t<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<description>Restart Button: Menu Item:" + menItem.name + "</description>");
        rawXML.AppendLine("\t\t<type>button</type>");
        rawXML.AppendLine("\t\t<id>" + topMenuId + "02</id>");
        rawXML.AppendLine("\t\t<posX>220</posX>");
        rawXML.AppendLine("\t\t<posY>640</posY>");
        rawXML.AppendLine("\t\t<onleft>" + topMenuId + "01" + "</onleft>");
        rawXML.AppendLine("\t\t<onright>" + topMenuId + "03" + "</onright>");
        rawXML.AppendLine("\t\t\t<onup>" + (menItem.id + 700).ToString() + "</onup>");
        rawXML.AppendLine("\t\t<width>40</width>");
        rawXML.AppendLine("\t\t<height>40</height>");
        rawXML.AppendLine("\t\t<textureNoFocus>restart_button.png</textureNoFocus>");
        rawXML.AppendLine("\t\t<textureFocus>restart_button.png</textureFocus>");
        rawXML.AppendLine("\t\t<action>98</action>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("\t<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("\t\t<unfocusedAlpha>200</unfocusedAlpha>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<description>Shutdown Button: Menu Item:" + menItem.name + "</description>");
        rawXML.AppendLine("\t\t<type>button</type>");
        rawXML.AppendLine("\t\t<id>" + topMenuId + "03</id>");
        rawXML.AppendLine("\t\t<posX>290</posX>");
        rawXML.AppendLine("\t\t<posY>640</posY>");
        rawXML.AppendLine("\t\t<onleft>" + topMenuId + "02</onleft>");
        rawXML.AppendLine("\t\t<onright>" + topMenuId + "01</onright>");
        rawXML.AppendLine("\t\t\t<onup>" + (menItem.id + 700).ToString() + "</onup>");
        rawXML.AppendLine("\t\t<width>40</width>");
        rawXML.AppendLine("\t\t<height>40</height>");
        rawXML.AppendLine("\t\t<textureNoFocus>shutdown_button.png</textureNoFocus>");
        rawXML.AppendLine("\t\t<textureFocus>shutdown_button.png</textureFocus>");
        rawXML.AppendLine("\t\t<action>99</action>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("\t<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("\t</control>");
        rawXML.AppendLine("</control>");
      }

      xml = xml.Replace("<!-- BEGIN GENERATED TOPBAR CODE -->", rawXML.ToString());
    }

    #endregion

    #region Generate Topbar Horizontal

    private void generateTopBarH()
    {
      int twitterHeight = 0;
      basicHomeValues.offsetButtons = (int.Parse(txtMenuPos.Text) - 8);

      StringBuilder rawXML = new StringBuilder();

      foreach (menuItem menItem in menuItems)
      {
        String topMenuId = (menItem.id + 600).ToString();

        rawXML.AppendLine("\n<control>\n\t<description>Topbar buttons " + menItem.name + "</description>");
        rawXML.AppendLine("\t<type>group</type>");
        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<description>Dummy label indicating " + menItem.name + " menu visibility</description>");
        rawXML.AppendLine("\t\t<id>" + (menItem.id + 100).ToString() + "</id>");
        rawXML.AppendLine("\t\t<type>label</type>");
        rawXML.AppendLine("\t\t<posX>100</posX>");
        rawXML.AppendLine("\t\t<posY>-100</posY>");
        rawXML.AppendLine("\t\t<width>500</width>");
        rawXML.AppendLine("\t\t<height>0</height>");
        rawXML.AppendLine("\t\t<label>" + menItem.name + "</label>");
        rawXML.AppendLine("\t\t<visible>Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<type>image</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>490</posX>");
        rawXML.AppendLine("\t\t<posY>-5</posY>");
        rawXML.AppendLine("\t\t<height>73</height>");
        rawXML.AppendLine("\t\t<width>300</width>");
        if (useAeonGraphics.Checked)
        {
          rawXML.AppendLine("\t\t<texture>3buttonbar-a.png</texture>");
        }
        else
        {
          rawXML.AppendLine("\t\t<texture>3buttonbar.png</texture>");
        }
        rawXML.AppendLine("\t\t<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<type>image</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>545</posX>");
        rawXML.AppendLine("\t\t<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("\t\t<height>50</height>");
        rawXML.AppendLine("\t\t<width>50</width>");
        rawXML.AppendLine("\t\t<texture>restart_button.png</texture>");
        rawXML.AppendLine("\t\t<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<type>image</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>615</posX>");
        rawXML.AppendLine("\t\t<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("\t\t<height>50</height>");
        rawXML.AppendLine("\t\t<width>50</width>");
        rawXML.AppendLine("\t\t<texture>exit_button.png</texture>");
        rawXML.AppendLine("\t\t<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<type>image</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>685</posX>");
        rawXML.AppendLine("\t\t<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("\t\t<height>50</height>");
        rawXML.AppendLine("\t\t<width>50</width>");
        rawXML.AppendLine("\t\t<texture>shutdown_button.png</texture>");
        rawXML.AppendLine("\t\t<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("\t</control>");


        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<description>BasicHome button button</description>");
        rawXML.AppendLine("\t\t<type>button</type>");
        rawXML.AppendLine("\t\t<id>" + topMenuId + "01</id>");
        rawXML.AppendLine("\t\t<posX>545</posX>");
        rawXML.AppendLine("\t\t<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("\t\t<width>50</width>");
        rawXML.AppendLine("\t\t<height>50</height>");
        rawXML.AppendLine("\t\t<textureFocus>restart_button.png</textureFocus>");
        rawXML.AppendLine("\t\t<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("\t\t<label>-</label>");
        rawXML.AppendLine("\t\t<action>98</action> ");
        rawXML.AppendLine("\t\t<onleft>" + topMenuId + "03</onleft>");
        rawXML.AppendLine("\t\t<onright>" + topMenuId + "02</onright>");
        rawXML.AppendLine("\t\t<onup>" + topMenuId + "01</onup>");
        rawXML.AppendLine("\t\t<ondown>" + (menItem.id + 700).ToString() + "</ondown>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("\t\t<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<description>Exit button</description>");
        rawXML.AppendLine("\t\t<type>button</type>");
        rawXML.AppendLine("\t\t<id>" + topMenuId + "02</id>");
        rawXML.AppendLine("\t\t<posX>615</posX>");
        rawXML.AppendLine("\t\t<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("\t\t<width>50</width>");
        rawXML.AppendLine("\t\t<height>50</height>");
        rawXML.AppendLine("\t\t<textureFocus>exit_button.png</textureFocus>");
        rawXML.AppendLine("\t\t<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("\t\t<label>-</label>");
        rawXML.AppendLine("\t\t<action>97</action>");
        rawXML.AppendLine("\t\t<onleft>" + topMenuId + "01</onleft>");
        rawXML.AppendLine("\t\t<onright>" + topMenuId + "03</onright>");
        rawXML.AppendLine("\t\t<onup>" + topMenuId + "02</onup>");
        rawXML.AppendLine("\t\t<ondown>" + (menItem.id + 700).ToString() + "</ondown>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("\t\t<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<description>Shutdown button</description>");
        rawXML.AppendLine("\t\t<type>button</type>");
        rawXML.AppendLine("\t\t<id>" + topMenuId + "03</id>");
        rawXML.AppendLine("\t\t<posX>685</posX>");
        rawXML.AppendLine("\t\t<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("\t\t<width>50</width>");
        rawXML.AppendLine("\t\t<height>50</height>");
        rawXML.AppendLine("\t\t<textureFocus>shutdown_button.png</textureFocus>");
        rawXML.AppendLine("\t\t<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("\t\t<label>-</label>");
        rawXML.AppendLine("\t\t<action>99</action>");
        rawXML.AppendLine("\t\t<onleft>" + topMenuId + "02</onleft>");
        rawXML.AppendLine("\t\t<onright>" + topMenuId + "01</onright>");
        rawXML.AppendLine("\t\t<onup>" + topMenuId + "03</onup>");
        rawXML.AppendLine("\t\t<ondown>" + (menItem.id + 700).ToString() + "</ondown>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("\t\t<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("\t</control>");
        rawXML.AppendLine("\t<animation effect=\"slide\" start=\"0,-" + basicHomeValues.Button3Slide.ToString() + "\" time=\"600\" acceleration=\"-0.1\" reversible=\"false\">visiblechange</animation>");

        rawXML.AppendLine("</control> <!-- /Topbar buttons " + menItem.name + " -->\n\n");
      }

      xml = xml.Replace("<!-- BEGIN GENERATED TOPBAR CODE -->", rawXML.ToString());
    }

    #endregion

    #region Crowding Fix Vertical

    private void generateCrowdingFixV()
    {
      //defaultId
      StringBuilder rawXML = new StringBuilder();
      int first = basicHomeValues.defaultId - 2;
      if (first < 0) first += menuItems.Count;

      int second = basicHomeValues.defaultId - 1;
      if (second < 0) second += menuItems.Count;

      int third = basicHomeValues.defaultId + 1;
      if (third >= menuItems.Count) third -= menuItems.Count;

      int fourth = basicHomeValues.defaultId + 2;
      if (fourth >= menuItems.Count) fourth -= menuItems.Count;

      const string quote = "\"";

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>home " + menuItems[basicHomeValues.defaultId].name + "</description>");
      rawXML.AppendLine("\t\t\t<type>button</type>");
      rawXML.AppendLine("\t\t\t<id>" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + "</id>");
      rawXML.AppendLine("\t\t\t<posX>0</posX>");
      rawXML.AppendLine("\t\t\t<posY>-30</posY>");
      rawXML.AppendLine("\t\t\t<label>" + menuItems[basicHomeValues.defaultId].name + "</label>");
      rawXML.AppendLine("\t\t\t<width>320</width>");
      rawXML.AppendLine("\t\t\t<height>72</height>");
      rawXML.AppendLine("\t\t\t<hyperlink>" + menuItems[basicHomeValues.defaultId].hyperlink + "</hyperlink>");
      rawXML.AppendLine("\t\t\t<textureFocus>-</textureFocus>");
      rawXML.AppendLine("\t\t\t<textureNoFocus>-</textureNoFocus>");
      rawXML.AppendLine("\t\t\t<hover>-</hover>");
      rawXML.AppendLine("\t<onleft>" + (menuItems[basicHomeValues.defaultId].id + 600).ToString() + "01</onleft>");
      rawXML.AppendLine("\t<onright>" + (menuItems[basicHomeValues.defaultId].id + 600).ToString() + "01</onright>");
      rawXML.AppendLine("\t\t\t<onup>" + (menuItems[second].id + 800).ToString() + "</onup>");
      rawXML.AppendLine("\t\t\t<ondown>" + (menuItems[third].id + 700).ToString() + "</ondown>");
      rawXML.AppendLine("\t\t\t<visible>control.isvisible(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("\t\t</control>");
      // ************ FIRST


      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>home " + menuItems[first].name + "</description>");
      rawXML.AppendLine("\t\t\t<type>label</type>");
      rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
      rawXML.AppendLine("\t\t\t<posY>142</posY>");
      rawXML.AppendLine("\t\t\t<width>320</width>");
      rawXML.AppendLine("\t\t\t<height>72</height>");
      rawXML.AppendLine("\t\t\t<label>" + menuItems[first].name + "</label>");
      rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
      rawXML.AppendLine("\t\t\t<align>right</align>");
      rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t</control>");

      // ************** SECOND



      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>home " + menuItems[second].name + "</description>");
      rawXML.AppendLine("\t\t\t<type>label</type>");
      rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
      rawXML.AppendLine("\t\t\t<posY>242</posY>");
      rawXML.AppendLine("\t\t\t<width>320</width>");
      rawXML.AppendLine("\t\t\t<height>72</height>");
      rawXML.AppendLine("\t\t\t<label>" + menuItems[second].name + "</label>");
      rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
      rawXML.AppendLine("\t\t\t<align>right</align>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("\t\t</control>");

      // ******** CENTER

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>home " + menuItems[basicHomeValues.defaultId].name + "</description>");
      rawXML.AppendLine("\t\t\t<type>label</type>");
      rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
      rawXML.AppendLine("\t\t\t<posY>342</posY>");
      rawXML.AppendLine("\t\t\t<width>320</width>");
      rawXML.AppendLine("\t\t\t<height>72</height>");
      rawXML.AppendLine("\t\t\t<label>" + menuItems[basicHomeValues.defaultId].name + "</label>");
      rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
      rawXML.AppendLine("\t\t\t<font>#selectedFont</font>");
      rawXML.AppendLine("\t\t\t<align>right</align>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("\t\t</control>");

      // ******** THIRD


      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>home " + menuItems[third].name + "</description>");
      rawXML.AppendLine("\t\t\t<type>label</type>");
      rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
      rawXML.AppendLine("\t\t\t<posY>442</posY>");
      rawXML.AppendLine("\t\t\t<width>320</width>");
      rawXML.AppendLine("\t\t\t<height>72</height>");
      rawXML.AppendLine("\t\t\t<label>" + menuItems[third].name + "</label>");
      rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
      rawXML.AppendLine("\t\t\t<align>right</align>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("\t\t</control>");

      // *************** FOURTH


      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>home " + menuItems[fourth].name + "</description>");
      rawXML.AppendLine("\t\t\t<type>label</type>");
      rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
      rawXML.AppendLine("\t\t\t<posY>542</posY>");
      rawXML.AppendLine("\t\t\t<width>320</width>");
      rawXML.AppendLine("\t\t\t<height>72</height>");
      rawXML.AppendLine("\t\t\t<label>" + menuItems[fourth].name + "</label>");
      rawXML.AppendLine("\t\t\t<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
      rawXML.AppendLine("\t\t\t<align>right</align>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("\t\t</control>");

      xml = xml.Replace("<!-- BEGIN CROWDING FIX CODE-->", rawXML.ToString());
    }

    #endregion

    #region Crowding Fix Horizontal

    private void generateCrowdingFixH()
    {
      //defaultId
      StringBuilder rawXML = new StringBuilder();
      int first = basicHomeValues.defaultId - 2;
      if (first < 0) first += menuItems.Count;

      int second = basicHomeValues.defaultId - 1;
      if (second < 0) second += menuItems.Count;

      int third = basicHomeValues.defaultId + 1;
      if (third >= menuItems.Count) third -= menuItems.Count;

      int fourth = basicHomeValues.defaultId + 2;
      if (fourth >= menuItems.Count) fourth -= menuItems.Count;

      rawXML.AppendLine("\n\n<control>");
      rawXML.AppendLine("\t<description>home " + menuItems[basicHomeValues.defaultId].name + "</description>");
      rawXML.AppendLine("\t<type>button</type>");
      // xxx
      rawXML.AppendLine("\t\t\t<id>" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + "</id>");
      rawXML.AppendLine("\t<posX>0</posX>");
      rawXML.AppendLine("\t<posY>-30</posY>");
      rawXML.AppendLine("\t<label>" + menuItems[basicHomeValues.defaultId].name + "</label>");
      rawXML.AppendLine("\t<width>320</width>");
      rawXML.AppendLine("\t<height>72</height>");
      rawXML.AppendLine("\t<hyperlink>" + menuItems[basicHomeValues.defaultId].hyperlink + "</hyperlink>");
      rawXML.AppendLine("\t<textureFocus>-</textureFocus>");
      rawXML.AppendLine("\t<textureNoFocus>-</textureNoFocus>");
      rawXML.AppendLine("\t<hover>-</hover>");
      rawXML.AppendLine("\t<onleft>" + (menuItems[second].id + 800).ToString() + "</onleft>");
      rawXML.AppendLine("\t<onright>" + (menuItems[third].id + 700).ToString() + "</onright>");
      rawXML.AppendLine("\t<onup>" + (menuItems[basicHomeValues.defaultId].id + 600).ToString() + "01</onup>");
      rawXML.AppendLine("\t<visible>control.isvisible(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
      rawXML.AppendLine("</control>	");
      // ************ FIRST


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("\t<description>home " + menuItems[first].name + "</description>");
      rawXML.AppendLine("\t<type>label</type>");
      rawXML.AppendLine("\t<posX>0</posX>");
      rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
      rawXML.AppendLine("\t<width>320</width>");
      rawXML.AppendLine("\t<height>72</height>");
      rawXML.AppendLine("\t<label>" + menuItems[first].name + "</label>");
      rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("\t<font>#labelFont</font>");
      rawXML.AppendLine("\t<align>center</align>");
      rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("\t<animation effect=\"slide\" start=\"-160,0\" end=\"-160,0\" time=\"4\" acceleration=\"-0.0\" reversible=\"false\">WindowOpen</animation><!-- needed to display item at negative offset -->");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      // ************** SECOND



      rawXML.AppendLine("<control>");
      rawXML.AppendLine("\t<description>home " + menuItems[second].name + "</description>");
      rawXML.AppendLine("\t<type>label</type>");
      rawXML.AppendLine("\t<posX>160</posX>");
      rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
      rawXML.AppendLine("\t<width>320</width>");
      rawXML.AppendLine("\t<height>72</height>");
      rawXML.AppendLine("\t<label>" + menuItems[second].name + "</label>");
      rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("\t<font>#labelFont</font>");
      rawXML.AppendLine("\t<align>center</align>");
      rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>	");

      // ******** CENTER
      if (cbDropShadow.Checked)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("\t<description>home " + menuItems[basicHomeValues.defaultId].name + "</description>");
        rawXML.AppendLine("\t<type>label</type>");
        rawXML.AppendLine("\t<posX>481</posX>");
        rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 1) + "</posY>");
        rawXML.AppendLine("\t<width>320</width>");
        rawXML.AppendLine("\t<height>72</height>");
        rawXML.AppendLine("\t<label>" + menuItems[basicHomeValues.defaultId].name + "</label>");
        rawXML.AppendLine("\t<textcolor>black</textcolor>");
        rawXML.AppendLine("\t<font>#selectedFont</font>");
        rawXML.AppendLine("\t<align>center</align>");
        rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
        rawXML.AppendLine("</control>	");
      }

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("\t<description>home " + menuItems[basicHomeValues.defaultId].name + "</description>");
      rawXML.AppendLine("\t<type>label</type>");
      rawXML.AppendLine("\t<posX>480</posX>");
      rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
      rawXML.AppendLine("\t<width>320</width>");
      rawXML.AppendLine("\t<height>72</height>");
      rawXML.AppendLine("\t<label>" + menuItems[basicHomeValues.defaultId].name + "</label>");
      rawXML.AppendLine("\t<textcolor>#menuitemFocus</textcolor>");
      rawXML.AppendLine("\t<font>#selectedFont</font>");
      rawXML.AppendLine("\t<align>center</align>");
      rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>	");

      // ******** THIRD


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("\t<description>home " + menuItems[third].name + "</description>");
      rawXML.AppendLine("\t<type>label</type>");
      rawXML.AppendLine("\t<posX>800</posX>");
      rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
      rawXML.AppendLine("\t<width>320</width>");
      rawXML.AppendLine("\t<height>72</height>");
      rawXML.AppendLine("\t<label>" + menuItems[third].name + "</label>");
      rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("\t<font>#labelFont</font>");
      rawXML.AppendLine("\t<align>center</align>");
      rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>	");

      // *************** FOURTH


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("\t<description>home " + menuItems[fourth].name + "</description>");
      rawXML.AppendLine("\t<type>label</type>");
      rawXML.AppendLine("\t<posX>1120</posX>");
      rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
      rawXML.AppendLine("\t<width>320</width>");
      rawXML.AppendLine("\t<height>72</height>");
      rawXML.AppendLine("\t<label>" + menuItems[fourth].name + "</label>");
      rawXML.AppendLine("\t<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("\t<font>#labelFont</font>");
      rawXML.AppendLine("\t<align>center</align>");
      rawXML.AppendLine("\t<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>	");

      xml = xml.Replace("<!-- BEGIN CROWDING FIX CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Item Backgrounds

    private void generateBg(menuType direction)
    {
      StringBuilder rawXML = new StringBuilder();

      foreach (backgroundItem item in bgItems)
      {
        //
        // Main controls - these deal with random fanart
        //
        rawXML.AppendLine("\n\t\t<control>");
        rawXML.AppendLine("\t\t\t<description>" + item.name + " BACKGROUND 1</description>");
        if (item.fanartHandlerEnabled)
          rawXML.AppendLine("\t\t\t<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "1</id>");
        else
          rawXML.AppendLine("\t\t\t<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "</id>");


        if (weatherBGlink.Checked && item.isWeather && infoserviceOptions.Enabled)
        {
          rawXML.AppendLine("\t\t\t<type>image</type>");
          rawXML.AppendLine("\t\t\t<texture>animations\\linkedweather\\#infoservice.weather.today.img.big.filenamewithoutext.jpg</texture>");
        }
        else
        {

          if (item.fanartHandlerEnabled)
          {
            rawXML.AppendLine("\t\t\t<type>image</type>");
            rawXML.AppendLine("\t\t\t<texture>#fanarthandler." + item.fanartPropery + ".backdrop1.any</texture>");

          }
          else
          {
            rawXML.AppendLine("\t\t\t<type>image</type>");
            rawXML.AppendLine("\t\t\t<texture>" + item.image + "</texture>");
          }

        }

        rawXML.AppendLine("\t\t\t<posx>0</posx>");
        rawXML.AppendLine("\t\t\t<posy>0</posy>");
        rawXML.AppendLine("\t\t\t<width>1280</width>");
        rawXML.AppendLine("\t\t\t<height>720</height>");
        if (item.fanartHandlerEnabled)
          rawXML.Append("\t\t\t<visible>[");
        else
          rawXML.Append("\t\t\t<visible>");


        for (int i = 0; i < item.ids.Count; i++)
        {
          if (i == 0)
            rawXML.Append("control.isvisible(" + item.ids[i] + ")");
          else
            rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
        }

        if (item.fanartHandlerEnabled)
          if (item.EnableMusicNowPlayingFanart)
            rawXML.Append("]+control.isvisible(91919297)+![control.isvisible(91919294)+Player.HasMedia]</visible>\n");
          else
            rawXML.Append("]+control.isvisible(91919297)</visible>\n");
        else
          rawXML.Append("</visible>\n");

        rawXML.AppendLine("\t\t\t<animation effect=\"fade\" start=\"10\" end=\"100\" time=\"1000\">VisibleChange</animation>");
        if (cbAnimateBackground.Checked)
        {
          rawXML.AppendLine("\t\t\t<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          rawXML.AppendLine("\t\t\t<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
        }
        rawXML.AppendLine("\t\t</control>");

        // Add second background control for random fanart provided 
        if (item.fanartHandlerEnabled)
        {
          rawXML.AppendLine("\n\t\t<control>");

          rawXML.AppendLine("\t\t\t<description>" + item.name + " BACKGROUND 2</description>");
          rawXML.AppendLine("\t\t\t<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "2</id>");
          rawXML.AppendLine("\t\t\t<type>image</type>");
          rawXML.AppendLine("\t\t\t<texture>#fanarthandler." + item.fanartPropery + ".backdrop2.any</texture>");
          rawXML.AppendLine("\t\t\t<posx>0</posx>");
          rawXML.AppendLine("\t\t\t<posy>0</posy>");
          rawXML.AppendLine("\t\t\t<width>1280</width>");
          rawXML.AppendLine("\t\t\t<height>720</height>");
          rawXML.Append("\t\t\t<visible>[");

          for (int i = 0; i < item.ids.Count; i++)
          {
            if (i == 0)
              rawXML.Append("control.isvisible(" + item.ids[i] + ")");
            else
              rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
          }

          if (item.EnableMusicNowPlayingFanart)
            rawXML.Append("]+control.isvisible(91919298)+![control.isvisible(91919294)+Player.HasMedia]</visible>\n");
          else
            rawXML.Append("]+control.isvisible(91919298)</visible>\n");

          rawXML.AppendLine("\t\t\t<animation effect=\"fade\" start=\"10\" end=\"100\" time=\"1000\">VisibleChange</animation>");
          if (cbAnimateBackground.Checked)
          {
            rawXML.AppendLine("\t\t\t<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          }
          rawXML.AppendLine("\t\t</control>");
        }

        //
        // Additional two controls that will be added if EnableMusicNowPlayingFanart is true, i.e. for thsi item if music is play display 
        // fanart for the artist.
        //
        if (item.EnableMusicNowPlayingFanart)
        {
          //
          // Control 1
          //
          rawXML.AppendLine("\n\t\t<control>");

          rawXML.AppendLine("\t\t\t<description>" + item.name + " NOW PLAYING BACKGROUND 1</description>");
          rawXML.AppendLine("\t\t\t<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "3</id>");
          rawXML.AppendLine("\t\t\t<type>image</type>");
          rawXML.AppendLine("\t\t\t<texture>#fanarthandler.music.backdrop1.play</texture>");
          rawXML.AppendLine("\t\t\t<posx>0</posx>");
          rawXML.AppendLine("\t\t\t<posy>0</posy>");
          rawXML.AppendLine("\t\t\t<width>1280</width>");
          rawXML.AppendLine("\t\t\t<height>720</height>");
          rawXML.Append("\t\t\t<visible>[");
          for (int i = 0; i < item.ids.Count; i++)
          {
            if (i == 0)
              rawXML.Append("control.isvisible(" + item.ids[i] + ")");
            else
              rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
          }
          rawXML.Append("]+Player.HasMedia+control.isvisible(91919295)</visible>\n");
          rawXML.AppendLine("\t\t\t<animation effect=\"fade\" start=\"10\" end=\"100\" time=\"1000\">VisibleChange</animation>");
          if (cbAnimateBackground.Checked)
          {
            rawXML.AppendLine("\t\t\t<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          }
          rawXML.AppendLine("\t\t</control>");

          //
          // Control 2
          //
          rawXML.AppendLine("\n\t\t<control>");

          rawXML.AppendLine("\t\t\t<description>" + item.name + " NOW PLAYING BACKGROUND 2</description>");
          rawXML.AppendLine("\t\t\t<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "4</id>");
          rawXML.AppendLine("\t\t\t<type>image</type>");
          rawXML.AppendLine("\t\t\t<texture>#fanarthandler.music.backdrop2.play</texture>");
          rawXML.AppendLine("\t\t\t<posx>0</posx>");
          rawXML.AppendLine("\t\t\t<posy>0</posy>");
          rawXML.AppendLine("\t\t\t<width>1280</width>");
          rawXML.AppendLine("\t\t\t<height>720</height>");
          rawXML.Append("\t\t\t<visible>[");
          for (int i = 0; i < item.ids.Count; i++)
          {
            if (i == 0)
              rawXML.Append("control.isvisible(" + item.ids[i] + ")");
            else
              rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
          }
          rawXML.Append("]+Player.HasMedia+control.isvisible(91919296)</visible>\n");
          rawXML.AppendLine("\t\t\t<animation effect=\"fade\" start=\"10\" end=\"100\" time=\"1000\">VisibleChange</animation>");
          if (cbAnimateBackground.Checked)
          {
            rawXML.AppendLine("\t\t\t<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          }
          rawXML.AppendLine("\t\t</control>");
        }

      }
      xml = xml.Replace("<!-- BEGIN GENERATED BACKGROUND CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Context Lables Horizontal

    private void GenerateContextLabelsH()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("\t\t<!-- Menu Context Labels -->");
      foreach (menuItem menItem in menuItems)
      {

        if (menItem.isDefault)
        {
          if (cbDropShadow.Checked)
          {
            // Add default label
            rawXML.AppendLine("\t\t<control>");
            rawXML.AppendLine("\t\t\t<description>" + menItem.name + " Label (Default)</description>");
            rawXML.AppendLine("\t\t\t<type>label</type>");
            rawXML.AppendLine("\t\t\t<posY>" + (int.Parse(txtMenuPos.Text) - 4) + "</posY>");
            rawXML.AppendLine("\t\t\t<posX>481</posX>");
            rawXML.AppendLine("\t\t\t<width>320</width>");
            rawXML.AppendLine("\t\t\t<height>24</height>");
            rawXML.AppendLine("\t\t\t<label>" + menItem.contextLabel + "</label>");
            rawXML.AppendLine("\t\t\t<textcolor>" + dropShadowColor + "</textcolor>");
            rawXML.AppendLine("\t\t\t<font>mediastream14tc</font>");
            rawXML.AppendLine("\t\t\t<align>center</align>");
            rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"400\" time=\"300\">Visible</animation>");
            rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
            rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 900).ToString() + ")</visible>");
            rawXML.AppendLine("\t\t</control>");
          }
          // Add default label
          rawXML.AppendLine("\t\t<control>");
          rawXML.AppendLine("\t\t\t<description>" + menItem.name + " Label (Default)</description>");
          rawXML.AppendLine("\t\t\t<type>label</type>");
          rawXML.AppendLine("\t\t\t<posY>" + (int.Parse(txtMenuPos.Text) - 5) + "</posY>");
          rawXML.AppendLine("\t\t\t<posX>480</posX>");
          rawXML.AppendLine("\t\t\t<width>320</width>");
          rawXML.AppendLine("\t\t\t<height>24</height>");
          rawXML.AppendLine("\t\t\t<label>" + menItem.contextLabel + "</label>");
          rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
          rawXML.AppendLine("\t\t\t<font>mediastream14tc</font>");
          rawXML.AppendLine("\t\t\t<align>center</align>");
          rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"400\" time=\"300\">Visible</animation>");
          rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 900).ToString() + ")</visible>");
          rawXML.AppendLine("\t\t</control>");
        }
        if (cbDropShadow.Checked)
        {
          rawXML.AppendLine("\t\t<control>");
          rawXML.AppendLine("\t\t\t<description>" + menItem.name + " Label</description>");
          rawXML.AppendLine("\t\t\t<type>label</type>");
          rawXML.AppendLine("\t\t\t<posY>" + (int.Parse(txtMenuPos.Text) - 4) + "</posY>");
          rawXML.AppendLine("\t\t\t<posX>481</posX>");
          rawXML.AppendLine("\t\t\t<width>320</width>");
          rawXML.AppendLine("\t\t\t<height>24</height>");
          rawXML.AppendLine("\t\t\t<label>" + menItem.contextLabel + "</label>");
          rawXML.AppendLine("\t\t\t<textcolor>" + dropShadowColor + "</textcolor>");
          rawXML.AppendLine("\t\t\t<font>mediastream14tc</font>");
          rawXML.AppendLine("\t\t\t<align>center</align>");
          rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
          rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
          rawXML.AppendLine("\t\t</control>");
        }
        rawXML.AppendLine("\t\t<control>");
        rawXML.AppendLine("\t\t\t<description>" + menItem.name + " Label</description>");
        rawXML.AppendLine("\t\t\t<type>label</type>");
        rawXML.AppendLine("\t\t\t<posY>" + (int.Parse(txtMenuPos.Text) - 5) + "</posY>");
        rawXML.AppendLine("\t\t\t<posX>480</posX>");
        rawXML.AppendLine("\t\t\t<width>320</width>");
        rawXML.AppendLine("\t\t\t<height>24</height>");
        rawXML.AppendLine("\t\t\t<label>" + menItem.contextLabel + "</label>");
        rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
        rawXML.AppendLine("\t\t\t<font>mediastream14tc</font>");
        rawXML.AppendLine("\t\t\t<align>center</align>");
        rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
        rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
        rawXML.AppendLine("\t\t</control>");
      }

      xml = xml.Replace("<!-- BEGIN CONTEXT LABELS CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Context Labels Vertical

    private void GenerateContextLabelsV()
    {
      StringBuilder rawXML = new StringBuilder();
      const string quote = "\"";

      rawXML.AppendLine("\t\t<!-- Menu Context Labels -->");
      foreach (menuItem menItem in menuItems)
      {
        if (menItem.isDefault)
        {
          // Add default label
          rawXML.AppendLine("\t\t<control>");
          rawXML.AppendLine("\t\t\t<description>" + menItem.name + " Label (Default)</description>");
          rawXML.AppendLine("\t\t\t<type>label</type>");
          rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
          rawXML.AppendLine("\t\t\t<posY>322</posY>");
          rawXML.AppendLine("\t\t\t<width>380</width>");
          rawXML.AppendLine("\t\t\t<height>72</height>");
          rawXML.AppendLine("\t\t\t<label>" + menItem.contextLabel + "</label>");
          rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
          rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
          rawXML.AppendLine("\t\t\t<align>right</align>");
          rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
          rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
          rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 900).ToString() + ")</visible>");
          rawXML.AppendLine("\t\t</control>");
        }

        rawXML.AppendLine("\t\t<control>");
        rawXML.AppendLine("\t\t\t<description>" + menItem.name + " Label</description>");
        rawXML.AppendLine("\t\t\t<type>label</type>");
        rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
        rawXML.AppendLine("\t\t\t<posY>322</posY>");
        rawXML.AppendLine("\t\t\t<width>380</width>");
        rawXML.AppendLine("\t\t\t<height>72</height>");
        rawXML.AppendLine("\t\t\t<label>" + menItem.contextLabel + "</label>");
        rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
        rawXML.AppendLine("\t\t\t<font>#labelFont</font>");
        rawXML.AppendLine("\t\t\t<align>right</align>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " delay=" + quote + "350" + quote + " time=" + quote + "250" + quote + ">Visible</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
        rawXML.AppendLine("\t\t</control>");

        // Display the current weather location under the menu option
        if (menItem.isWeather)
        {
          rawXML.AppendLine("\t\t<control>");
          rawXML.AppendLine("\t\t\t<description>" + menItem.name + " Label</description>");
          rawXML.AppendLine("\t\t\t<type>label</type>");
          rawXML.AppendLine("\t\t\t<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
          rawXML.AppendLine("\t\t\t<posY>395</posY>");
          rawXML.AppendLine("\t\t\t<width>380</width>");
          rawXML.AppendLine("\t\t\t<height>62</height>");
          rawXML.AppendLine("\t\t\t<label> in #infoservice.weather.location</label>");
          rawXML.AppendLine("\t\t\t<textcolor>#menuitemFocus</textcolor>");
          rawXML.AppendLine("\t\t\t<font>mediastream10tc</font>");
          rawXML.AppendLine("\t\t\t<align>right</align>");
          rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " delay=" + quote + "350" + quote + " time=" + quote + "250" + quote + ">Visible</animation>");
          rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
          rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
          rawXML.AppendLine("\t\t\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")]</visible>");
          rawXML.AppendLine("\t\t</control>");
        }


      }

      xml = xml.Replace("<!-- BEGIN CONTEXT LABELS CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Five Day Weather

    private void GenerateFiveDayWeather()
    {
      if (enableFiveDayWeather.Checked == true)
      {
        foreach (backgroundItem item in bgItems)
        {
          if (item.isWeather)
          {
            basicHomeValues.weatherControl = (int.Parse(item.ids[0]) + 200);
            if (item.fanartHandlerEnabled)
              basicHomeValues.weatherControl = int.Parse((int.Parse(item.ids[0]) + 200).ToString(0 + "1"));

            if (menuStyle == chosenMenuStyle.verticalStyle)
              generateFiveDayWeatherVertical(basicHomeValues.weatherControl);
            else if (weatherStyle == chosenWeatherStyle.bottom)
              generateFiveDayWeatherStyle1(basicHomeValues.weatherControl);
            else if (weatherStyle == chosenWeatherStyle.middle)
              generateFiveDayWeatherStyle2(basicHomeValues.weatherControl);
          }
        }
      }
    }

    #endregion

    #region Generate Five Day Weather Vertical

    private void generateFiveDayWeatherVertical(int weatherId)
    {
      StringBuilder rawXML = new StringBuilder();

      int xPos1 = 660;
      int yPos1 = 70;
      int xPos2 = 455;
      int yPos2 = 415;
      int i = 0;
      int spacing = 250;

      // Create dummy label to be used with basichome nowplaying overlay
      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>5-Day Weather Dummy Label</description>");
      rawXML.AppendLine("\t<type>label</type>");
      rawXML.AppendLine("\t<id>6767</id>");
      rawXML.AppendLine("\t<posX>-50</posX>");
      rawXML.AppendLine("\t<posY>-50</posY>");
      rawXML.AppendLine("\t<label></label>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>GROUP: Forecast BGs</description>");
      rawXML.AppendLine("\t<type>group</type>");
      rawXML.AppendLine("\t<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");


      // ******************************** Weather Backgrounds **********************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>TODAY BG</description>");
      rawXML.AppendLine("\t\t<posX>" + (xPos1 - 200).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 155).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<type>image</type>");
      rawXML.AppendLine("\t\t<id>6777</id>");
      rawXML.AppendLine("\t\t<width>180</width>");
      rawXML.AppendLine("\t\t<height>270</height>");
      rawXML.AppendLine("\t\t<texture>weather2.png</texture>");
      rawXML.AppendLine("\t</control>");
      for (i = 1; i < 5; i++)
      {
        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY " + i.ToString() + " BG</description>");
        if (i < 3)
        {
          rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * i)).ToString() + "</posX>");
          rawXML.AppendLine("\t\t<posY>" + yPos1.ToString() + "</posY>");
        }
        else
        {
          rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * (i - 2))).ToString() + "</posX>");
          rawXML.AppendLine("\t\t<posY>" + yPos2.ToString() + "</posY>");
        }
        rawXML.AppendLine("\t\t<type>image</type>");
        rawXML.AppendLine("\t\t<id>6777</id>");
        rawXML.AppendLine("\t\t<width>180</width>");
        rawXML.AppendLine("\t\t<height>270</height>");
        rawXML.AppendLine("\t\t<texture>weather2.png</texture>");
        rawXML.AppendLine("\t</control>");
      }
      rawXML.AppendLine("</control>");

      // ********************************* Weather Icons **************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>TODAY ICON</description>");
      rawXML.AppendLine("\t\t<id>0</id>");
      if (WeatherIconsAnimated.Checked)
      {
        rawXML.AppendLine("\t\t<type>multiimage</type>");
        rawXML.AppendLine("\t\t<imagepath>" + weatherIcon(0) + "</imagepath>");
        rawXML.AppendLine("\t\t<timeperimage>33</timeperimage>");
        rawXML.AppendLine("\t\t<loop>True</loop>");
      }
      else
      {
        rawXML.AppendLine("\t\t<type>image</type>");
        rawXML.AppendLine("\t\t<texture>" + weatherIcon(0) + "</texture>");
      }
      rawXML.AppendLine("\t\t<posX>" + (xPos1 - 200).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + ((yPos1 + 155) - 70).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<height>180</height>");
      rawXML.AppendLine("\t\t<width>180</width>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");

      rawXML.AppendLine("\t\t<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      rawXML.AppendLine("\t</control>");

      for (i = 1; i < 5; i++)
      {
        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY " + i.ToString() + " ICON</description>");
        rawXML.AppendLine("\t\t<id>0</id>");
        if (WeatherIconsAnimated.Checked)
        {
          rawXML.AppendLine("\t\t<type>multiimage</type>");
          rawXML.AppendLine("\t\t<imagepath>" + weatherIcon(i) + "</imagepath>");
          rawXML.AppendLine("\t\t<timeperimage>33</timeperimage>");
          rawXML.AppendLine("\t\t<loop>True</loop>");
        }
        else
        {
          rawXML.AppendLine("\t\t<type>image</type>");
          rawXML.AppendLine("\t\t<texture>" + weatherIcon(i) + "</texture>");
        }
        if (i < 3)
        {
          rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * i)).ToString() + "</posX>");
          rawXML.AppendLine("\t\t<posY>" + (yPos1 - 70).ToString() + "</posY>");
        }
        else
        {
          rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * (i - 2))).ToString() + "</posX>");
          rawXML.AppendLine("\t\t<posY>" + (yPos2 - 70).ToString() + "</posY>");
        }
        rawXML.AppendLine("\t\t<height>180</height>");
        rawXML.AppendLine("\t\t<width>180</width>");
        if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
        else
          rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
        rawXML.AppendLine("\t\t<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
        rawXML.AppendLine("\t</control>");
      }
      // ************************************* Weather Text Items *******************************
      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>GROUP: FULL WEATHER DETAILS</description>");
      rawXML.AppendLine("\t<type>group</type>");
      rawXML.AppendLine("\t<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");

      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      // ************************************* Day 1 ****************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 1 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + ((xPos1 - 200) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 245 + 155).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>6030</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 1 TEMP</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + ((xPos1 - 200) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 180 + 155).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("\t\t<font>mediastream28tc</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      if (getInfoServiceVersion().CompareTo("1.6.0.0") < 0)
      {
        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY 1 MAX VALUE</description>");
        rawXML.AppendLine("\t\t<type>label</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>" + ((xPos1 - 200) + 5).ToString() + "</posX>");
        rawXML.AppendLine("\t\t<posY>" + (yPos1 + 200 + 155).ToString() + "</posY>");
        rawXML.AppendLine("\t\t<align>left</align>");
        rawXML.AppendLine("\t\t<label>#infoservice.weather.today.maxtemp</label>");
        rawXML.AppendLine("\t\t<font>mediastream12</font>");
        rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY 1 MIN VALUE</description>");
        rawXML.AppendLine("\t\t<type>label</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>" + ((xPos1 - 200) + 175).ToString() + "</posX>");
        rawXML.AppendLine("\t\t<posY>" + (yPos1 + 200 + 155).ToString() + "</posY>");
        rawXML.AppendLine("\t\t<align>right</align>");
        rawXML.AppendLine("\t\t<label>#infoservice.weather.today.mintemp</label>");
        rawXML.AppendLine("\t\t<font>mediastream12</font>");
        rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
        rawXML.AppendLine("\t</control>");
      }

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 1 GENERAL WEATHER</description>");
      rawXML.AppendLine("\t\t<type>textbox</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + ((xPos1 - 200) + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 100 + 155).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<height>60</height>");
      rawXML.AppendLine("\t\t<font>mediastream13</font>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("\t</control>");
      // ************************************* Day 2 ****************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + spacing + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 245).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream12</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + spacing + 175).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream12</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>textbox</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 100).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<height>50</height>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>textbox</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 150).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<height>50</height>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      // ************************************* Day 3 ****************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * 2) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 245).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * 2) + 175).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 100).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos1 + 150).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      // ************************************* Day 4 ****************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + spacing + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos2 + 245).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + spacing + 175).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos2 + 100).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos2 + 150).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      // ************************************* Day 5 ****************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * 2) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos2 + 245).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * 2) + 175).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos2 + 100).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("\t\t<posY>" + (yPos2 + 150).ToString() + "</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED FIVE DAY WEATHER CODE -->", rawXML.ToString());
    }

    #endregion

    #region Generate Five Day Weather Horizontal (Style 1)

    private void generateFiveDayWeatherStyle1(int weatherId)
    {
      int i = 0;
      int xPos1 = 120;
      int yPos1 = 544;
      int spacing = 210;

      StringBuilder rawXML = new StringBuilder();

      // Create dummy label to be used with basichome nowplaying overlay
      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>5-Day Weather Dummy Label</description>");
      rawXML.AppendLine("\t<type>label</type>");
      rawXML.AppendLine("\t<id>6767</id>");
      rawXML.AppendLine("\t<posX>-50</posX>");
      rawXML.AppendLine("\t<posY>-50</posY>");
      rawXML.AppendLine("\t<label></label>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      rawXML.AppendLine("</control>");

      // Group for Weather Backgrounds
      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>GROUP: FIVE DAY WEATHER BACKGROUNDS</description>");
      rawXML.AppendLine("\t<type>group</type>");
      rawXML.AppendLine("\t<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY " + i.ToString() + " BG</description>");
        rawXML.AppendLine("\t<posX>" + (xPos1 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("\t<posY>" + yPos1.ToString() + "</posY>");
        rawXML.AppendLine("\t\t<type>image</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<width>200</width>");
        rawXML.AppendLine("\t\t<height>170</height>");
        rawXML.AppendLine("\t\t<texture>weather2.png</texture>");
        rawXML.AppendLine("\t</control>");

      }
      rawXML.AppendLine("</control>");

      //Group for weather Icons
      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>GROUP: FIVE DAY WEATHER ICONS</description>");
      rawXML.AppendLine("\t<type>group</type>");
      rawXML.AppendLine("\t<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      // ********************* Weather Icons **************************************
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY " + i.ToString() + " ICON</description>");
        rawXML.AppendLine("\t\t<id>0</id>");
        if (WeatherIconsAnimated.Checked)
        {
          rawXML.AppendLine("\t\t<type>multiimage</type>");
          rawXML.AppendLine("\t\t<imagepath>" + weatherIcon(i) + "</imagepath>");
          rawXML.AppendLine("\t\t<timeperimage>33</timeperimage>");
          rawXML.AppendLine("\t\t<loop>True</loop>");
        }
        else
        {
          rawXML.AppendLine("\t\t<type>image</type>");
          rawXML.AppendLine("\t\t<texture>" + weatherIcon(i) + "</texture>");
        }
        rawXML.AppendLine("\t\t<posX>" + (xPos1 + 20 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("\t\t<posY>" + (yPos1 - 4).ToString() + "</posY>");
        rawXML.AppendLine("\t\t<height>80</height>");
        rawXML.AppendLine("\t\t<width>80</width>");
        if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")]</visible>");
        else
          rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
        rawXML.AppendLine("\t\t<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
        rawXML.AppendLine("\t</control>");

        // This control was for debugging only, it displays the weather condition number
        //rawXML.AppendLine("\n\t<control>");
        //rawXML.AppendLine("\t\t<description>DAY 1 LABEL</description>");
        //rawXML.AppendLine("\t\t<type>label</type>");
        //rawXML.AppendLine("\t\t<id>0</id>");
        //rawXML.AppendLine("\t\t<posX>100</posX>");
        //rawXML.AppendLine("\t\t<posY>" + (200 + (30 * i)).ToString() + "</posY>");
        //rawXML.AppendLine("\t\t<width>800</width>");
        //rawXML.AppendLine("\t\t<align>center</align>");
        //rawXML.AppendLine("\t\t<label>" + weatherIcon(i) + "</label>");
        //rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
        //rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
        //rawXML.AppendLine("\t</control>");

      }
      rawXML.AppendLine("</control>");



      //Group for weather Text
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>GROUP: FIVE DAY WEATHER TEXT</description>");
      rawXML.AppendLine("\t\t<type>group</type>");
      rawXML.AppendLine("\t\t<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      rawXML.AppendLine("\t\t<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")]</visible>");
      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      // **************************************** DAY 1 *********************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 1 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>120</posX>");
      rawXML.AppendLine("\t\t<posY>699</posY>");
      rawXML.AppendLine("\t\t<width>200</width>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>6030</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 1 TEMP</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>300</posX>");
      rawXML.AppendLine("\t\t<posY>615</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      if (getInfoServiceVersion().CompareTo("1.6.0.0") < 0)
      {
        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY 1 MAX VALUE</description>");
        rawXML.AppendLine("\t\t<type>label</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>300</posX>");
        rawXML.AppendLine("\t\t<posY>559</posY>");
        rawXML.AppendLine("\t\t<align>right</align>");
        rawXML.AppendLine("\t\t<label>#infoservice.weather.today.maxtemp</label>");
        rawXML.AppendLine("\t\t<font>mediastream11</font>");
        rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY 1 MIN VALUE</description>");
        rawXML.AppendLine("\t\t<type>label</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>300</posX>");
        rawXML.AppendLine("\t\t<posY>584</posY>");
        rawXML.AppendLine("\t\t<align>right</align>");
        rawXML.AppendLine("\t\t<label>#infoservice.weather.today.mintemp</label>");
        rawXML.AppendLine("\t\t<font>mediastream11</font>");
        rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
        rawXML.AppendLine("\t</control>");
      }

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 1 GENERAL WEATHER</description>");
      rawXML.AppendLine("\t\t<type>textbox</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>140</posX>");
      rawXML.AppendLine("\t\t<posY>634</posY>");
      rawXML.AppendLine("\t\t<width>140</width>");
      rawXML.AppendLine("\t\t<height>50</height>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t</control>");
      // **************************************** DAY 2 *****************************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>330</posX>");
      rawXML.AppendLine("\t\t<posY>699</posY>");
      rawXML.AppendLine("\t\t<width>200</width>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>510</posX>");
      rawXML.AppendLine("\t\t<posY>559</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>510</posX>");
      rawXML.AppendLine("\t\t<posY>584</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>350</posX>");
      rawXML.AppendLine("\t\t<posY>634</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>350</posX>");
      rawXML.AppendLine("\t\t<posY>654</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      // **************************************** DAY 3 ***********************************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>540</posX>");
      rawXML.AppendLine("\t\t<posY>699</posY>");
      rawXML.AppendLine("\t\t<width>200</width>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>720</posX>");
      rawXML.AppendLine("\t\t<posY>559</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>720</posX>");
      rawXML.AppendLine("\t\t<posY>584</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>560</posX>");
      rawXML.AppendLine("\t\t<posY>634</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>560</posX>");
      rawXML.AppendLine("\t\t<posY>654</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      // **************************************** DAY 4 ***********************************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>750</posX>");
      rawXML.AppendLine("\t\t<posY>699</posY>");
      rawXML.AppendLine("\t\t<width>200</width>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>930</posX>");
      rawXML.AppendLine("\t\t<posY>559</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>930</posX>");
      rawXML.AppendLine("\t\t<posY>584</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>770</posX>");
      rawXML.AppendLine("\t\t<posY>634</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>770</posX>");
      rawXML.AppendLine("\t\t<posY>654</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      // **************************************** DAY 5 ***********************************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>960</posX>");
      rawXML.AppendLine("\t\t<posY>699</posY>");
      rawXML.AppendLine("\t\t<width>200</width>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>1140</posX>");
      rawXML.AppendLine("\t\t<posY>559</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>1140</posX>");
      rawXML.AppendLine("\t\t<posY>584</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>980</posX>");
      rawXML.AppendLine("\t\t<posY>634</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>980</posX>");
      rawXML.AppendLine("\t\t<posY>654</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED FIVE DAY WEATHER CODE -->", rawXML.ToString());
    }

    #endregion

    #region Generate Five Day Weather Horizontal (Style 2)

    private void generateFiveDayWeatherStyle2(int weatherId)
    {
      int i = 0;
      int xPos1 = 120;
      int yPos1 = 150;
      int spacing = 210;
      StringBuilder rawXML = new StringBuilder();

      // Group for Weather Backgrounds
      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>GROUP: Forecast BGs</description>");
      rawXML.AppendLine("\t<type>group</type>");
      rawXML.AppendLine("\t<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY " + i.ToString() + " BG</description>");
        rawXML.AppendLine("\t<posX>" + (xPos1 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("\t<posY>" + yPos1.ToString() + "</posY>");
        rawXML.AppendLine("\t\t<type>image</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<width>180</width>");
        rawXML.AppendLine("\t\t<height>270</height>");
        rawXML.AppendLine("\t\t<texture>weather2.png</texture>");
        rawXML.AppendLine("\t</control>");
      }
      rawXML.AppendLine("</control>");

      //Group for weather Icons
      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>GROUP: FULL WEATHER ICONS (Animated)</description>");
      rawXML.AppendLine("\t<type>group</type>");
      rawXML.AppendLine("\t<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      // ********************* Weather Icons **************************************
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY " + i.ToString() + " ICON</description>");
        rawXML.AppendLine("\t\t<id>0</id>");
        if (WeatherIconsAnimated.Checked)
        {
          rawXML.AppendLine("\t\t<type>multiimage</type>");
          rawXML.AppendLine("\t\t<imagepath>" + weatherIcon(i) + "</imagepath>");
          rawXML.AppendLine("\t\t<timeperimage>33</timeperimage>");
          rawXML.AppendLine("\t\t<loop>True</loop>");
        }
        else
        {
          rawXML.AppendLine("\t\t<type>image</type>");
          rawXML.AppendLine("\t\t<texture>" + weatherIcon(i) + "</texture>");
        }
        rawXML.AppendLine("\t\t<posX>" + (xPos1 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("\t\t<posY>" + (yPos1 - 70).ToString() + "</posY>");
        rawXML.AppendLine("\t\t<height>180</height>");
        rawXML.AppendLine("\t\t<width>180</width>");
        if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
        else
          rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
        rawXML.AppendLine("\t<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
        rawXML.AppendLine("\t</control>");
      }
      rawXML.AppendLine("</control>");
      // Group for all text parts
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>GROUP: FULL WEATHER</description>");
      rawXML.AppendLine("\t\t<type>group</type>");
      rawXML.AppendLine("\t\t<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      rawXML.AppendLine("\t\t<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      // ************************************* Day 1 ****************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 1 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>210</posX>");
      rawXML.AppendLine("\t\t<posY>394</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>6030</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 1 TEMP</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>210</posX>");
      rawXML.AppendLine("\t\t<posY>310</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("\t\t<font>mediastream28tc</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      if (getInfoServiceVersion().CompareTo("1.6.0.0") < 0)
      {
        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY 1 MAX VALUE</description>");
        rawXML.AppendLine("\t\t<type>label</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>125</posX>");
        rawXML.AppendLine("\t\t<posY>355</posY>");
        rawXML.AppendLine("\t\t<align>left</align>");
        rawXML.AppendLine("\t\t<label>#infoservice.weather.today.maxtemp</label>");
        rawXML.AppendLine("\t\t<font>mediastream12</font>");
        rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
        rawXML.AppendLine("\t</control>");

        rawXML.AppendLine("\n\t<control>");
        rawXML.AppendLine("\t\t<description>DAY 1 MIN VALUE</description>");
        rawXML.AppendLine("\t\t<type>label</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>295</posX>");
        rawXML.AppendLine("\t\t<posY>355</posY>");
        rawXML.AppendLine("\t\t<align>right</align>");
        rawXML.AppendLine("\t\t<label>#infoservice.weather.today.mintemp</label>");
        rawXML.AppendLine("\t\t<font>mediastream12</font>");
        rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
        rawXML.AppendLine("\t</control>");
      }

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 1 GENERAL WEATHER</description>");
      rawXML.AppendLine("\t\t<type>textbox</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>125</posX>");
      rawXML.AppendLine("\t\t<posY>230</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<height>60</height>");
      rawXML.AppendLine("\t\t<font>mediastream13</font>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("\t</control>");
      // ************************************* Day 2 ****************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>420</posX>");
      rawXML.AppendLine("\t\t<posY>394</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>335</posX>");
      rawXML.AppendLine("\t\t<posY>355</posY>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream12</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>505</posX>");
      rawXML.AppendLine("\t\t<posY>355</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream12</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>textbox</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>340</posX>");
      rawXML.AppendLine("\t\t<posY>230</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<height>50</height>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 2 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>textbox</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>340</posX>");
      rawXML.AppendLine("\t\t<posY>285</posY>");
      rawXML.AppendLine("\t\t<width>160</width>");
      rawXML.AppendLine("\t\t<height>50</height>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "2.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      // ************************************* Day 3 ****************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>630</posX>");
      rawXML.AppendLine("\t\t<posY>394</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>545</posX>");
      rawXML.AppendLine("\t\t<posY>355</posY>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>715</posX>");
      rawXML.AppendLine("\t\t<posY>355</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>545</posX>");
      rawXML.AppendLine("\t\t<posY>230</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 3 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>545</posX>");
      rawXML.AppendLine("\t\t<posY>285</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "3.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      // ************************************* Day 4 ****************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>840</posX>");
      rawXML.AppendLine("\t\t<posY>394</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>755</posX>");
      rawXML.AppendLine("\t\t<posY>355</posY>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>925</posX>");
      rawXML.AppendLine("\t\t<posY>355</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>755</posX>");
      rawXML.AppendLine("\t\t<posY>230</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 4 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>755</posX>");
      rawXML.AppendLine("\t\t<posY>285</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "4.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      // ************************************* Day 5 ****************************************
      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 LABEL</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>1050</posX>");
      rawXML.AppendLine("\t\t<posY>394</posY>");
      rawXML.AppendLine("\t\t<align>center</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.weekday</label>");
      rawXML.AppendLine("\t\t<font>mediastream11tc</font>");
      rawXML.AppendLine("\t\t<textcolor>White</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 MAX VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>965</posX>");
      rawXML.AppendLine("\t\t<posY>355</posY>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.maxtemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 MIN VALUE</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>1135</posX>");
      rawXML.AppendLine("\t\t<posY>355</posY>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.mintemp</label>");
      rawXML.AppendLine("\t\t<font>mediastream11</font>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 AM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>965</posX>");
      rawXML.AppendLine("\t\t<posY>230</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.day.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\n\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 5 PM WEATHER</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>965</posX>");
      rawXML.AppendLine("\t\t<posY>285</posY>");
      rawXML.AppendLine("\t\t<width>170</width>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather." + infoServiceDayProperty + "5.night.condition</label>");
      rawXML.AppendLine("\t\t<font>mediastream10</font>");
      rawXML.AppendLine("\t\t<align>left</align>");
      rawXML.AppendLine("\t\t<textcolor>white</textcolor>");
      rawXML.AppendLine("\t</control>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED FIVE DAY WEATHER CODE -->", rawXML.ToString());
    }

    #endregion

    #region Generate Weather Summary

    private void generateWeathersummary(int weatherId)
    {

      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>GROUP: WEATHER SUMMARY</description>");
      rawXML.AppendLine("\t<type>group</type>");
      rawXML.AppendLine("\t<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)+!control.isvisible(" + int.Parse(weatherId.ToString()) + ")</visible>");
      rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");

      rawXML.AppendLine("\t<control>");
      rawXML.AppendLine("\t\t<description>DAY 0 BG</description>");
      rawXML.AppendLine("\t\t<type>image</type>");
      rawXML.AppendLine("\t\t<id>0</id>");
      rawXML.AppendLine("\t\t<posX>976</posX>");
      rawXML.AppendLine("\t\t<posY>-3</posY>");
      rawXML.AppendLine("\t\t<width>306</width>");
      rawXML.AppendLine("\t\t<height>75</height>");
      rawXML.AppendLine("\t\t<texture>homeweatheroverlaybg.png</texture>");
      rawXML.AppendLine("\t</control>");

      if (WeatherIconsAnimated.Checked)
      {
        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<description>Todays weather image (Animated Version)</description>");
        rawXML.AppendLine("\t\t<type>multiimage</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>993</posX>");
        rawXML.AppendLine("\t\t<posY>9</posY>");
        rawXML.AppendLine("\t\t<height>54</height>");
        rawXML.AppendLine("\t\t<width>54</width>");
        rawXML.AppendLine("\t\t<centered>no</centered>");
        rawXML.AppendLine("\t\t<texture>-</texture>");
        rawXML.AppendLine("\t\t<imagepath>" + weatherIcon(0) + "</imagepath>");
        rawXML.AppendLine("\t\t<timeperimage>33</timeperimage>");
        rawXML.AppendLine("\t\t<loop>True</loop>");
        rawXML.AppendLine("\t</control>");
      }
      else
      {
        rawXML.AppendLine("\t<control>");
        rawXML.AppendLine("\t\t<description>Todays weather image (Animated Version)</description>");
        rawXML.AppendLine("\t\t<type>image</type>");
        rawXML.AppendLine("\t\t<id>0</id>");
        rawXML.AppendLine("\t\t<posX>993</posX>");
        rawXML.AppendLine("\t\t<posY>9</posY>");
        rawXML.AppendLine("\t\t<height>54</height>");
        rawXML.AppendLine("\t\t<width>54</width>");
        rawXML.AppendLine("\t\t<centered>no</centered>");
        rawXML.AppendLine("\t\t<texture>" + weatherIcon(0) + "</texture>");
        rawXML.AppendLine("\t</control>");

      }
      rawXML.AppendLine("\t<control>");
      rawXML.AppendLine("\t\t<description>Temperature</description>");
      rawXML.AppendLine("\t\t<type>label</type>");
      rawXML.AppendLine("\t\t<id>1</id>");
      rawXML.AppendLine("\t\t<width>400</width>");
      rawXML.AppendLine("\t\t<height>50</height>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<posX>1265</posX>");
      rawXML.AppendLine("\t\t<posY>30</posY>");
      rawXML.AppendLine("\t\t<font>mediastream14c</font>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("\t<control>");
      rawXML.AppendLine("\t\t<description>Todays Weather Condition</description>");
      rawXML.AppendLine("\t\t<type>textbox</type>");
      rawXML.AppendLine("\t\t<id>1</id>");
      rawXML.AppendLine("\t\t<width>150</width>");
      rawXML.AppendLine("\t\t<height>50</height>");
      rawXML.AppendLine("\t\t<align>right</align>");
      rawXML.AppendLine("\t\t<posX>1055</posX>");
      rawXML.AppendLine("\t\t<posY>15</posY>");
      rawXML.AppendLine("\t\t<font>mediastream10tc</font>");
      rawXML.AppendLine("\t\t<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("\t</control>");

      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED WEATHER SUMMARY CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Clock

    private void generateClock()
    {
      StringBuilder rawXML = new StringBuilder();
      rawXML.AppendLine("<import>common.time.xml</import>");
      xml = xml.Replace("<!-- BEGIN COMMON TIME CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Fanart Scraper Progress

    private void generatefanartScraper()
    {
      StringBuilder rawXML = new StringBuilder();
      if (menuStyle == chosenMenuStyle.verticalStyle)
        rawXML.AppendLine("<import>common.fanart.scraperv.xml</import>");
      else
        rawXML.AppendLine("<import>common.fanart.scraper.xml</import>");
      xml = xml.Replace("<!-- BEGIN FANART SCRAPER CODE-->", rawXML.ToString());
    }

    #endregion

    #region InfoSerice Most Recent Includes

    void generateMostRecentInclude(isOverlayType overlayType)
    {
      StringBuilder rawXML = new StringBuilder();
      string replaceString = null;

      if (overlayType == isOverlayType.TVSeries)
      {
        replaceString = "<!-- BEGIN MOST RECENT TVSERIES CODE-->";

        if (menuStyle == chosenMenuStyle.verticalStyle)
        {
          if (tvSeriesRecentStyle == tvSeriesRecentType.full)
            rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.VFull.xml</import>");
          else
            rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.VSum.xml</import>");
        }
        else
        {
          if (tvSeriesRecentStyle == tvSeriesRecentType.full)
            rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.HFull.xml</import>");
          else
            rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.HSum.xml</import>");
        }
      }

      if (overlayType == isOverlayType.MovPics)
      {
        replaceString = "<!-- BEGIN MOST RECENT MOVPICS CODE-->";

        if (menuStyle == chosenMenuStyle.verticalStyle)
        {
          if (movPicsRecentStyle == movPicsRecentType.full)
            rawXML.AppendLine("<import>basichome.recentlyadded.movpics.HFull.xml</import>");
          else
            rawXML.AppendLine("<import>basichome.recentlyadded.movpics.HFull.xml</import>");
        }
        else
        {
          if (movPicsRecentStyle == movPicsRecentType.full)
            rawXML.AppendLine("<import>basichome.recentlyadded.movpics.HFull.xml</import>");
          else
            rawXML.AppendLine("<import>basichome.recentlyadded.movpics.HFull.xml</import>");
        }
      }
      xml = xml.Replace(replaceString, rawXML.ToString());
    }

    #endregion

    #region Generate Twitter Horizontal

    private void generateTwitter()
    {
      StringBuilder rawXML = new StringBuilder();


      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>Menu Twitter Sub Menu</description>");
      rawXML.AppendLine("\t<type>image</type>");
      rawXML.AppendLine("\t<id>99004</id>");
      rawXML.AppendLine("\t<posX>280</posX>");
      rawXML.AppendLine("\t<posY>400</posY>");
      rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter).ToString() + "</posY>");
      rawXML.AppendLine("\t<width>1000</width>");
      rawXML.AppendLine("\t<height>" + basicHomeValues.subMenuTopHeight + "</height>");
      rawXML.AppendLine("\t<texture>" + basicHomeValues.mymenu_submenutop + "</texture>");
      rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)</visible>");
      if (wrapString.Checked)
      {
        if (useInfoServiceSeparator)
          //rawXML.AppendLine("\t<wrapString> #infoservice.feed.separator </wrapString>");
          rawXML.AppendLine("\t<wrapString> :: </wrapString>");
        else
          rawXML.AppendLine("\t<wrapString> :: </wrapString>");
      }

      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,320" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>Twitter image</description>");
      rawXML.AppendLine("\t<type>image</type>");
      rawXML.AppendLine("\t<id>0</id>");
      rawXML.AppendLine("\t<width>28</width>");
      rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) - (basicHomeValues.offsetTwitterImage - 5)).ToString() + "</posY>");
      rawXML.AppendLine("\t<posX>330</posX>");
      rawXML.AppendLine("\t<texture>InfoService\\defaultTwitter.png</texture>");
      rawXML.AppendLine("\t<keepaspectratio>yes</keepaspectratio>");
      rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,320" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>Twitter Items</description>");
      rawXML.AppendLine("\t<type>fadelabel</type>");
      rawXML.AppendLine("\t<id>1</id>");
      rawXML.AppendLine("\t<width>1160</width>");
      rawXML.AppendLine("\t<height>50</height>");
      rawXML.AppendLine("\t<posY>" + (int.Parse(txtMenuPos.Text) - (basicHomeValues.offsetTwitterImage - 6)).ToString() + "</posY>");
      rawXML.AppendLine("\t<posX>360</posX>");
      rawXML.AppendLine("\t<font>mediastream12</font>");
      rawXML.AppendLine("\t<textcolor>ff000000</textcolor>");
      rawXML.AppendLine("\t<label>#infoservice.twitter.messages</label>");
      rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,320" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED TWITTER CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Twitter Vertical

    private void generateTwitterV()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>Menu Twitter Sub Menu</description>");
      rawXML.AppendLine("\t<type>image</type>");
      rawXML.AppendLine("\t<id>99004</id>");
      rawXML.AppendLine("\t<posX>140</posX>");
      rawXML.AppendLine("\t<posY>9</posY>");
      rawXML.AppendLine("\t<width>840</width>");
      rawXML.AppendLine("\t<height>34</height>");
      rawXML.AppendLine("\t<texture>" + basicHomeValues.mymenu_submenutop + "</texture>");
      rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)</visible>");
      if (wrapString.Checked)
      {
        if (useInfoServiceSeparator)
          //rawXML.AppendLine("\t<wrapString> #infoservice.feed.separator </wrapString>");
          rawXML.AppendLine("\t<wrapString> :: </wrapString>");
        else
          rawXML.AppendLine("\t<wrapString> :: </wrapString>");
      }

      rawXML.AppendLine("</control>");

      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>Twitter image</description>");
      rawXML.AppendLine("\t<type>image</type>");
      rawXML.AppendLine("\t<id>18</id>");
      rawXML.AppendLine("\t<width>20</width>");
      rawXML.AppendLine("\t<posY>16</posY>");
      rawXML.AppendLine("\t<posX>149</posX>");
      rawXML.AppendLine("\t<texture>InfoService\\defaultTwitter.png</texture>");
      rawXML.AppendLine("\t<keepaspectratio>yes</keepaspectratio>");
      rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("\n<control>");
      rawXML.AppendLine("\t<description>Twitter Items</description>");
      rawXML.AppendLine("\t<type>fadelabel</type>");
      rawXML.AppendLine("\t<id>1</id>");
      rawXML.AppendLine("\t<width>775</width>");
      rawXML.AppendLine("\t<height>50</height>");
      rawXML.AppendLine("\t<posY>15</posY>");
      rawXML.AppendLine("\t<posX>175</posX>");
      rawXML.AppendLine("\t\t\t<font>mediastream12tc</font>");
      rawXML.AppendLine("\t<label>#infoservice.twitter.messages</label>");
      rawXML.AppendLine("\t<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED TWITTER CODE-->", rawXML.ToString());
    }

    #endregion

    #region Write UserMenuProfile.xml

    private void writeMenuProfile(menuType direction)
    {
      string menuPos;
      string menuOrientation;
      string activeMenuStyle = null;
      string activeWeatherStyle = null;
      string acceleration = tbAcceleration.Text;
      string duration = tbDuration.Text;
      string activeRssImageType = null;
      string targetScreenRes = "SD";
      string tvRecentDisplayType = "full";
      string movPicsDisplayType = "full";

      string settingDropShadow = cbDropShadow.Checked ? "true" : "false";
      string settingEnableRssfeed = enableRssfeed.Checked ? "true" : "false";
      string settingEnableTwitter = enableTwitter.Checked ? "true" : "false";
      string settingWrapString = wrapString.Checked ? "true" : "false";
      string settingWeatherBGlink = weatherBGlink.Checked ? "true" : "false";
      string settingFiveDayWeatherCheckBox = enableFiveDayWeather.Checked ? "true" : "false";
      string settingSummaryWeatherCheckBox = summaryWeatherCheckBox.Checked ? "true" : "false";
      string settingClearCacheOnGenerate = cboClearCache.Checked ? "true" : "false";
      string settingAnimatedWeather = WeatherIconsAnimated.Checked ? "true" : "false";
      string settingHorizontalContextLabels = horizontalContextLabels.Checked ? "true" : "false";
      string settingFullWeatherSummaryBottom = fullWeatherSummaryBottom.Checked ? "true" : "false";
      string settingFullWeatherSummaryMiddle = fullWeatherSummaryMiddle.Checked ? "true" : "false";
      string disableOnScreenClock = cbDisableClock.Checked ? "true" : "false";
      string hideFanartScrapingtext = cbHideFanartScraper.Checked ? "true" : "false";
      string enableOverlayFanart = cbOverlayFanart.Checked ? "true" : "false";
      string animatedBackground = cbAnimateBackground.Checked ? "true" : "false";
      string tvSeriesMostRecent = cbMostRecentTvSeries.Checked ? "true" : "false";
      string movPicsMostRecent = cbMostRecentMovPics.Checked ? "true" : "false";


      if (direction == menuType.horizontal)
      {
        menuPos = "menuYPos";
        menuOrientation = "Horizontal";
      }
      else
      {
        menuPos = "menuXPos";
        menuOrientation = "Vertical";
      }

      switch (menuStyle)
      {
        case chosenMenuStyle.verticalStyle:
          activeMenuStyle = "verticalStyle";
          break;
        case chosenMenuStyle.horizontalStandardStyle:
          activeMenuStyle = "horizontalStandardStyle";
          break;
        case chosenMenuStyle.horizontalContextStyle:
          activeMenuStyle = "horizontalContextStyle";
          break;
      }

      switch (rssImage)
      {
        case rssImageType.infoserviceImage:
          activeRssImageType = "infoservice";
          break;
        case rssImageType.noImage:
          activeRssImageType = "noimage";
          break;
        case rssImageType.skinImage:
          activeRssImageType = "skin";
          break;
      }

      if (weatherStyle == chosenWeatherStyle.bottom)
        activeWeatherStyle = "bottom";
      else if (weatherStyle == chosenWeatherStyle.middle)
        activeWeatherStyle = "middle";

      if (screenres == screenResolutionType.res1920x1080)
        targetScreenRes = "HD";
      else
        targetScreenRes = "SD";

      if (tvSeriesRecentStyle == tvSeriesRecentType.summary)
        tvRecentDisplayType = "summary";

      if (movPicsRecentStyle == movPicsRecentType.summary)
        movPicsDisplayType = "summary";

      xml = ("<profile>\n\t"
                + "<version>1.0</version>\n\t"
                + "<skin name=\"StreamedMP\">\n\t"
                + "\t<section name=" + quote + "StreamedMP Options" + quote + ">\n"
                + generateEntry("menustyle", activeMenuStyle, 2, true)
                + generateEntry("weatherstyle", activeWeatherStyle, 2, true)
                + generateEntry("menuitemFocus", focusAlpha.Text + txtFocusColour.Text, 2, true)
                + generateEntry("menuitemNoFocus", noFocusAlpha.Text + txtNoFocusColour.Text, 2, true)
                + generateEntry("labelFont", cboLabelFont.Text, 2, true)
                + generateEntry("selectedFont", cboSelectedFont.Text, 2, true)
                + generateEntry("menuType", menuOrientation, 2, true)
                + generateEntry(menuPos, txtMenuPos.Text, 2, true)
                + generateEntry("acceleration", acceleration, 2, true)
                + generateEntry("duration", duration, 2, true)
                + generateEntry("dropShadow", settingDropShadow, 2, true)
                + generateEntry("enableRssfeed", settingEnableRssfeed, 2, true)
                + generateEntry("enableTwitter", settingEnableTwitter, 2, true)
                + generateEntry("wrapString", settingWrapString, 2, true)
                + generateEntry("weatherBGlink", settingWeatherBGlink, 2, true)
                + generateEntry("fiveDayWeatherCheckBox", settingFiveDayWeatherCheckBox, 2, true)
                + generateEntry("summaryWeatherCheckBox", settingSummaryWeatherCheckBox, 2, true)
                + generateEntry("cboClearCache", settingClearCacheOnGenerate, 2, true)
                + generateEntry("animatedWeather", settingAnimatedWeather, 2, true)
                + generateEntry("horizontalContextLabels", settingHorizontalContextLabels, 2, true)
                + generateEntry("fullWeatherSummaryBottom", settingFullWeatherSummaryBottom, 2, true)
                + generateEntry("fullWeatherSummaryMiddle", settingFullWeatherSummaryMiddle, 2, true)
                + generateEntry("activeRssImageType", activeRssImageType, 2, true)
                + generateEntry("disableOnScreenClock", disableOnScreenClock, 2, true)
                + generateEntry("targetScreenRes", targetScreenRes, 2, true)
                + generateEntry("hideFanartScrapingtext", hideFanartScrapingtext, 2, true)
                + generateEntry("enableOverlayFanart", enableOverlayFanart, 2, true)
                + generateEntry("animatedBackground", animatedBackground, 2, true)
                + generateEntry("tvSeriesMostRecent", tvSeriesMostRecent, 2, true)
                + generateEntry("movPicsMostRecent", movPicsMostRecent, 2, true)
                + generateEntry("tvRecentDisplayType", tvRecentDisplayType, 2, true)
                + generateEntry("movPicsDisplayType", movPicsDisplayType, 2, true)
                + "\t</section>");

      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("\n\t<!-- End Of Menu Options -->\n\t<section name=" + quote + "StreamedMP Menu Items" + quote + ">");

      int menuIndex = 0;
      rawXML.AppendLine(generateEntry("count", menuItems.Count.ToString(), 2, false));
      foreach (menuItem menItem in menuItems)
      {
        rawXML.AppendLine("\t\t<!-- Menu Entry : " + menuIndex.ToString() + " -->");
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "name", menItem.name, 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "label", menItem.contextLabel, 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "folder", menItem.bgFolder, 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "fanartproperty", menItem.fanartProperty, 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "fanarthandlerenabled", menItem.fanartHandlerEnabled.ToString(), 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "enablemusicnowplayingfanart", menItem.EnableMusicNowPlayingFanart.ToString(), 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "hyperlink", menItem.hyperlink, 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "isdefault", menItem.isDefault.ToString(), 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "isweather", menItem.isWeather.ToString(), 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "id", menItem.id.ToString(), 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "updatestatus", menItem.updateStatus.ToString(), 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "defaultimage", menItem.defaultImage, 2, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "disableBGSharing", menItem.disableBGSharing.ToString(), 2, false));


        menuIndex += 1;
      }
      rawXML.AppendLine("\t</section>");
      rawXML.AppendLine("</skin>");
      rawXML.AppendLine("</profile>");

      xml += rawXML.ToString();

      if (System.IO.File.Exists(mpPaths.configBasePath + "usermenuprofile.xml"))
        System.IO.File.Copy(mpPaths.configBasePath + "usermenuprofile.xml", mpPaths.configBasePath + "usermenuprofile.xml.backup." + DateTime.Now.Ticks.ToString());

      if (System.IO.File.Exists(mpPaths.configBasePath + "usermenuprofile.xml"))
        System.IO.File.Delete(mpPaths.configBasePath + "usermenuprofile.xml");

      StreamWriter writer;
      writer = System.IO.File.CreateText(mpPaths.configBasePath + "usermenuprofile.xml");
      writer.Write(xml);
      writer.Close();
    }
    #endregion
  }
}