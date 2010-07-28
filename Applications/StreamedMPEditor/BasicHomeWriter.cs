using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;

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

      if (fanartHandlerUsed)
      {
        xml = xml.Replace("<!-- BEGIN GENERATED DEFINITIONS -->"
                        , "<define>#menuitemFocus:" + focusAlpha.Text + txtFocusColour.Text + "</define>"
                        + "<define>#menuitemNoFocus:" + noFocusAlpha.Text + txtNoFocusColour.Text + "</define>"
                        + "<define>#labelFont:" + cboLabelFont.Text + "</define>"
                        + "<define>#selectedFont:" + cboSelectedFont.Text + "</define>"
                        + "<define>#" + menuPos + "</define>"
                        + "<define>#useRandomGamesFanart:" + randomFanartGames + "</define>"
                        + "<define>#useRandomTVSeriesFanart:" + randomFanartTVSeries + "</define>"
                        + "<define>#useRandomPluginsFanart:" + randomFanartPlugins + "</define>"
                        + "<define>#useRandomMovingPicturesFanart:" + randomFanartMovingPictures + "</define>"
                        + "<define>#useRandomMusicFanart:" + randomFanartMusic + "</define>"
                        + "<define>#useRandomPicturesFanart:" + randomFanartPictures + "</define>"
                        + "<define>#useRandomTVFanart:" + randomFanartTv + "</define>"
                        + "<define>#useRandomMoviesFanart:" + randomFanartMovies + "</define>"
                        + "<define>#useRandomScoreCenterFanart:" + randomScoreCenterFanart + "</define>");
      }
      else
      {
        xml = xml.Replace("<!-- BEGIN GENERATED DEFINITIONS -->"
                        , "<define>#menuitemFocus:" + focusAlpha.Text + txtFocusColour.Text + "</define>"
                        + "<define>#menuitemNoFocus:" + noFocusAlpha.Text + txtNoFocusColour.Text + "</define>"
                        + "<define>#labelFont:" + cboLabelFont.Text + "</define>"
                        + "<define>#selectedFont:" + cboSelectedFont.Text + "</define>"
                        + "<define>#" + menuPos + "</define>");

      }



      StringBuilder rawXML = new StringBuilder();
      foreach (menuItem menItem in menuItems)
      {

        fillBackgroundItem(menItem);

        if (menItem.isDefault == true)
          xml = xml.Replace("<!-- BEGIN GENERATED DEFAULTCONTROL CODE-->", "<defaultcontrol>" + (menItem.id + 900).ToString() + "</defaultcontrol>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Dummy label indicating " + menItem.name + " visibility</description>");
        rawXML.AppendLine("<id>" + menItem.id.ToString() + "</id>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>100</posX>");
        rawXML.AppendLine("<posY>-100</posY>");
        rawXML.AppendLine("<width>500</width>");
        rawXML.AppendLine("<height>0</height>");
        rawXML.AppendLine("<label>Movies</label>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")|Control.HasFocus(" + (menItem.id + 900).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")</visible>");
        rawXML.AppendLine("</control>");


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
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>button</type>");
                rawXML.AppendLine("<id>" + (menItem.id + 700).ToString() + "</id>");
                rawXML.AppendLine("<posX>0</posX>");
                rawXML.AppendLine("<posY>-30</posY>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");
                if (menItem.isDefault)
                {
                  rawXML.AppendLine("<width>1280</width>");
                  rawXML.AppendLine("<height>720</height>");
                }
                else
                {
                  rawXML.AppendLine("<width>320</width>");
                  rawXML.AppendLine("<height>72</height>");
                }
                rawXML.AppendLine("<textureFocus>-</textureFocus>");
                rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");

                if (menItem.hyperlink == "196299")
                  rawXML.AppendLine("<action>99</action>");
                else if (menItem.hyperlink == "196297")
                  rawXML.AppendLine("<action>97</action>");
                else
                  rawXML.AppendLine("<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");


                rawXML.AppendLine("<hover>-</hover>");

                if (menuItems.IndexOf(menItem) == 0)
                  onleft = menuItems[menuItems.Count - 1].id;
                else
                  onleft = (menItem.id - 1);
                if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                  onright = menuItems[0].id;
                else
                  onright = (menItem.id + 1);
                rawXML.AppendLine("<onleft>" + (onleft + 800).ToString() + "</onleft>");
                rawXML.AppendLine("<onright>" + (onright + 700).ToString() + "</onright>");
                rawXML.AppendLine("<onup>" + (menItem.id + 600).ToString() + "01</onup>");
                rawXML.AppendLine("<visible>control.isvisible(" + (menItem.id + 700).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;

              case 1:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>button</type>");
                rawXML.AppendLine("<id>" + (menItem.id + 800).ToString() + "</id>");
                rawXML.AppendLine("<posX>0</posX>");
                rawXML.AppendLine("<posY>-30</posY>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textureFocus>-</textureFocus>");
                rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");

                if (menItem.hyperlink == "196299")
                  rawXML.AppendLine("<action>99</action>");
                else if (menItem.hyperlink == "196297")
                  rawXML.AppendLine("<action>97</action>");
                else
                  rawXML.AppendLine("<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");

                rawXML.AppendLine("<hover>-</hover>");

                if (menuItems.IndexOf(menItem) == 0)
                  onleft = menuItems[menuItems.Count - 1].id;
                else
                  onleft = (menItem.id - 1);
                if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                  onright = menuItems[0].id;
                else
                  onright = (menItem.id + 1);
                rawXML.AppendLine("<onleft>" + (onleft + 800).ToString() + "</onleft>");
                rawXML.AppendLine("<onright>" + (onright + 700).ToString() + "</onright>");
                rawXML.AppendLine("<onup>" + (menItem.id + 600).ToString() + "01</onup>");
                //rawXML.AppendLine("<ondown>" + (menItem.id + 700).ToString() + "01</ondown>");
                rawXML.AppendLine("<visible>control.isvisible(" + (menItem.id + 800).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;

              case 2:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>-160</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 2)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 3)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[2].id + 700).ToString() + ")|control.isvisible(" + (menuItems[2].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|control.isvisible(" + (menItem.id + 101).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 3:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>1120</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 2 == -2)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 2].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 2 == -1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 698).ToString() + ")|control.isvisible(" + (menItem.id + 98).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 4:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>800</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 1 == -1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 699).ToString() + ")|control.isvisible(" + (menItem.id + 99).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 5:
                if (cbDropShadow.Checked)
                {
                  rawXML.AppendLine("<control>");
                  rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                  rawXML.AppendLine("<type>label</type>");
                  rawXML.AppendLine("<posX>482</posX>");
                  rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 2) + "</posY>");
                  rawXML.AppendLine("<width>320</width>");
                  rawXML.AppendLine("<height>72</height>");
                  rawXML.AppendLine("<textcolor>" + dropShadowColor + "</textcolor>");
                  rawXML.AppendLine("<font>#selectedFont</font>");
                  rawXML.AppendLine("<align>center</align>");
                  rawXML.AppendLine("<label>" + menItem.name + "</label>");

                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")</visible>");

                  rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                  rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                  rawXML.AppendLine("</control>");

                }
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>480</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
                rawXML.AppendLine("<font>#selectedFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 6:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>160</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|control.isvisible(" + (menItem.id + 101).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 7:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>0</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 702).ToString() + ")|control.isvisible(" + (menItem.id + 102).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "160,0" + quote + " end=" + quote + "-160,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 8:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>1120</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 3 == -3)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 3].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 3 == -2)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 3 == -1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 3).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 9:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>800</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 2 == -2)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 2 == -1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 2).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,0" + quote + " end=" + quote + "320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 10:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>800</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 1 == -1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 1).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 11:
                if (cbDropShadow.Checked)
                {
                  rawXML.AppendLine("<control>");
                  rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                  rawXML.AppendLine("<type>label</type>");
                  rawXML.AppendLine("<posX>481</posX>");
                  rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 1) + "</posY>");
                  rawXML.AppendLine("<width>320</width>");
                  rawXML.AppendLine("<height>72</height>");
                  rawXML.AppendLine("<textcolor>" + dropShadowColor + "</textcolor>");
                  rawXML.AppendLine("<font>#selectedFont</font>");
                  rawXML.AppendLine("<align>center</align>");
                  rawXML.AppendLine("<label>" + menItem.name + "</label>");
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
                  rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                  rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                  rawXML.AppendLine("</control>");

                }
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>480</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
                rawXML.AppendLine("<font>#selectedFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");
                rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 12:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>160</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) + 1).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-320,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 13:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>0</posX>");
                rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>center</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) + 2).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-480,0" + quote + " end=" + quote + "-160,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
                rawXML.AppendLine("</control>");
                break;
            }

          }
          else if (direction == menuType.vertical)
          {
            switch (i)
            {
              case 0:

                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>button</type>");
                rawXML.AppendLine("<id>" + (menItem.id + 700).ToString() + "</id>");
                rawXML.AppendLine("<posX>0</posX>");
                rawXML.AppendLine("<posY>-30</posY>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");
                if (menItem.isDefault)
                {
                  rawXML.AppendLine("<width>1280</width>");
                  rawXML.AppendLine("<height>720</height>");
                }
                else
                {
                  rawXML.AppendLine("<width>320</width>");
                  rawXML.AppendLine("<height>72</height>");
                }
                rawXML.AppendLine("<textureFocus>-</textureFocus>");
                rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");

                if (menItem.hyperlink == "196299")
                  rawXML.AppendLine("<action>99</action>");
                else if (menItem.hyperlink == "196297")
                  rawXML.AppendLine("<action>97</action>");
                else
                  rawXML.AppendLine("<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");

                rawXML.AppendLine("<hover>-</hover>");

                if (menuItems.IndexOf(menItem) == 0)
                  onleft = menuItems[menuItems.Count - 1].id;
                else
                  onleft = (menItem.id - 1);
                if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                  onright = menuItems[0].id;
                else
                  onright = (menItem.id + 1);
                if (cbExitStyleOld.Checked)
                {
                  rawXML.AppendLine("<onright>7777</onright>");
                  rawXML.AppendLine("<onleft>-</onleft>");
                }
                else
                {
                  rawXML.AppendLine("<onright>" + (menItem.id + 600).ToString() + "01</onright>");
                  rawXML.AppendLine("<onleft>" + (menItem.id + 600).ToString() + "01</onleft>");
                }
                rawXML.AppendLine("<onup>" + (onleft + 800).ToString() + "</onup>");
                rawXML.AppendLine("<ondown>" + (onright + 700).ToString() + "</ondown>");
                rawXML.AppendLine("<visible>control.isvisible(" + (menItem.id + 700).ToString() + ")</visible>");
                rawXML.AppendLine("</control>");
                break;

              case 1:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>button</type>");
                rawXML.AppendLine("<id>" + (menItem.id + 800).ToString() + "</id>");
                rawXML.AppendLine("<posX>0</posX>");
                rawXML.AppendLine("<posY>-30</posY>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");
                rawXML.AppendLine("<width>320</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textureFocus>-</textureFocus>");
                rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");

                if (menItem.name.ToLower() == "shutdown")
                  rawXML.AppendLine("<action>99</action>");
                else if (menItem.name.ToLower() == "exit")
                  rawXML.AppendLine("<action>97</action>");
                else
                  rawXML.AppendLine("<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");

                rawXML.AppendLine("<hover>-</hover>");

                if (menuItems.IndexOf(menItem) == 0)
                  onleft = menuItems[menuItems.Count - 1].id;
                else
                  onleft = (menItem.id - 1);
                if (menuItems.IndexOf(menItem) == menuItems.Count - 1)
                  onright = menuItems[0].id;
                else
                  onright = (menItem.id + 1);
                if (cbExitStyleOld.Checked)
                {
                  rawXML.AppendLine("<onright>7777</onright>");
                  rawXML.AppendLine("<onleft>-</onleft>");
                }
                else
                {
                  rawXML.AppendLine("<onright>" + (menItem.id + 600).ToString() + "01</onright>");
                  rawXML.AppendLine("<onleft>" + (menItem.id + 600).ToString() + "01</onleft>");
                }
                rawXML.AppendLine("<onleft>" + (menItem.id + 600).ToString() + "01</onleft>");
                rawXML.AppendLine("<onup>" + (onleft + 800).ToString() + "</onup>");
                rawXML.AppendLine("<ondown>" + (onright + 700).ToString() + "</ondown>");
                rawXML.AppendLine("<visible>control.isvisible(" + (menItem.id + 800).ToString() + ")</visible>");
                rawXML.AppendLine("</control>");
                break;

              case 2:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>-24</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 2)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 3 == menuItems.Count + 3)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[2].id + 700).ToString() + ")|control.isvisible(" + (menuItems[2].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|control.isvisible(" + (menItem.id + 101).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,118" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 3:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>542</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 2 == -2)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 2].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 2 == -1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 698).ToString() + ")|control.isvisible(" + (menItem.id + 98).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 4:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>442</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 1 == -1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[menuItems.Count - 1].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 699).ToString() + ")|control.isvisible(" + (menItem.id + 99).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 5:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>342</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
                rawXML.AppendLine("<font>#selectedFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");
                rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 6:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>242</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")|control.isvisible(" + (menItem.id + 101).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 7:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>142</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")|control.isvisible(" + (menuItems[1].id + 100).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")|control.isvisible(" + (menuItems[0].id + 100).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 702).ToString() + ")|control.isvisible(" + (menItem.id + 102).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 8:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>-24</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 3 == -3)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 3].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 3 == -2)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 3 == -1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 3).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-118" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 9:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>542</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 2 == -2)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) - 2 == -1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 2).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 10:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>442</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) - 1 == -1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 1).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 11:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>342</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
                rawXML.AppendLine("<font>#selectedFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");
                rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 12:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>242</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) + 1).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
                break;
              case 13:
                rawXML.AppendLine("<control>");
                rawXML.AppendLine("<description>" + menItem.name + i.ToString() + "</description>");
                rawXML.AppendLine("<type>label</type>");
                rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
                rawXML.AppendLine("<posY>142</posY>");
                rawXML.AppendLine("<width>500</width>");
                rawXML.AppendLine("<height>72</height>");
                rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
                rawXML.AppendLine("<font>#labelFont</font>");
                rawXML.AppendLine("<align>right</align>");
                rawXML.AppendLine("<label>" + menItem.name + "</label>");

                if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 800).ToString() + ")</visible>");
                else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
                  rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
                else
                  rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) + 2).ToString() + ")</visible>");

                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
                rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
                rawXML.AppendLine("</control>");
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
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Menu Background</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>99003</id>");
      rawXML.AppendLine("<posX>0</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu) + "</posY>");
      rawXML.AppendLine("<width>1280</width>");
      rawXML.AppendLine("<height>" + basicHomeValues.menuHeight + "</height>");
      rawXML.AppendLine("<texture>" + basicHomeValues.mymenu + "</texture>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");
      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Menu Sub Menu</description>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>99004</id>");
        rawXML.AppendLine("<posX>" + basicHomeValues.subMenuXpos + "</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu).ToString() + "</posY>");
        rawXML.AppendLine("<width>" + basicHomeValues.subMenuWidth + "</width>");
        rawXML.AppendLine("<height>" + basicHomeValues.subMenuHeight.ToString() + "</height>");
        rawXML.AppendLine("<texture>" + basicHomeValues.mymenu_submenu + "</texture>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
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
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Menu Background</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) - maxXPosition).ToString() + "</posX>");
      rawXML.AppendLine("<posY>0</posY>");
      rawXML.AppendLine("<width>1280</width>"); rawXML.AppendLine("<height>720</height>");
      rawXML.AppendLine("<texture>basichome.menu.overlay.png</texture>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>!Control.HasFocus(7888)+!Control.HasFocus(7999)+!Control.HasFocus(7777)</visible>");
      rawXML.AppendLine("</control>");
      if (enableRssfeed.Checked)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Rss Background</description>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>1</id>");
        rawXML.AppendLine("<posX>80</posX>");
        rawXML.AppendLine("<posY>685</posY>");
        rawXML.AppendLine("<width>1300</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<texture>homerss.png</texture>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,100" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+!string.equals(#infoservice.feed.titles,)</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Weather Background</description>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>1</id>");
        rawXML.AppendLine("<posX>976</posX>");
        rawXML.AppendLine("<posY>-3</posY>");
        rawXML.AppendLine("<width>306</width>");
        rawXML.AppendLine("<height>75</height>");
        rawXML.AppendLine("<texture>homeweatheroverlaybg.png</texture>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.hasthumb(43001)</visible>");
        rawXML.AppendLine("</control>");
      }

      xml = xml.Replace("<!-- BEGIN GENERATED MENUGRAPHICS CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Weather Vertical

    private void generateWeatherV()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Weather image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>43001</id>");
      rawXML.AppendLine("<posX>987</posX>");
      rawXML.AppendLine("<posY>9</posY>");
      rawXML.AppendLine("<height>53</height>");
      rawXML.AppendLine("<width>55</width>");
      rawXML.AppendLine("<centered>yes</centered>");
      rawXML.AppendLine("<texture>#infoservice.weather.today.img.small.fullpath</texture>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Temperature</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>400</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<posX>1250</posX>");
      rawXML.AppendLine("<posY>22</posY>");
      rawXML.AppendLine("<font>mediastream14c</font>");
      rawXML.AppendLine("<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>condition</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>400</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<posX>1048</posX>");
      rawXML.AppendLine("<posY>15</posY>");
      rawXML.AppendLine("<font>mediastream10tc</font>");
      rawXML.AppendLine("<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

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
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>RSS Feed image (InfoService)</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<id>1</id>");
          rawXML.AppendLine("<keepaspectratio>yes</keepaspectratio>");
          rawXML.AppendLine("<height>26</height>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssImage + rssImageYposOffset).ToString() + "</posY>");
          rawXML.AppendLine("<posX>" + (rssImageXposOffset + 60).ToString() + "</posX>");
          rawXML.AppendLine("<texture>#infoservice.feed.img</texture>");
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case rssImageType.skinImage:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>RSS Feed image (Default Skin Image)</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<id>1</id>");
          rawXML.AppendLine("<width>24</width>");
          rawXML.AppendLine("<height>24</height>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssImage + 4 + rssImageYposOffset).ToString() + "</posY>");
          rawXML.AppendLine("<posX>60</posX>");
          rawXML.AppendLine("<texture>InfoService\\defaultFeedRSS.png</texture>");
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
      }

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>RSS Items</description>");
      rawXML.AppendLine("<type>fadelabel</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>1280</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssText).ToString() + "</posY>");
      if (rssImage == rssImageType.skinImage)
        rawXML.AppendLine("<posX>90</posX>");
      else if (rssImage == rssImageType.infoserviceImage)
        rawXML.AppendLine("<posX>140</posX>");
      else
        rawXML.AppendLine("<posX>60</posX>");
      rawXML.AppendLine("<font>mediastream12</font>");
      rawXML.AppendLine("<textcolor>ff000000</textcolor>");
      rawXML.AppendLine("<label>#infoservice.feed.titles</label>");
      if (wrapString.Checked)
      {
        if (useInfoServiceSeparator)
          //rawXML.AppendLine("<wrapString> #infoservice.feed.separator </wrapString>");
          rawXML.AppendLine("<wrapString> :: </wrapString>");
        else
          rawXML.AppendLine("<wrapString> :: </wrapString>");
      }
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED RSS TICKER CODE-->", rawXML.ToString());

    }
    #endregion

    #region Generate RSSTicker Vertical

    private void generateRSSTickerV()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>RSS Items</description>");
      rawXML.AppendLine("<type>fadelabel</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>1250</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<posY>695</posY>");
      rawXML.AppendLine("<posX>120</posX>");
      rawXML.AppendLine("<font>mediastream12tc</font>");
      rawXML.AppendLine("<label>#infoservice.feed.titles</label>");
      if (wrapString.Checked)
      {
        if (useInfoServiceSeparator)
          //rawXML.AppendLine("<wrapString> #infoservice.feed.separator </wrapString>");
          rawXML.AppendLine("<wrapString> :: </wrapString>");
        else
          rawXML.AppendLine("<wrapString> :: </wrapString>");
      }
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,100" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,100" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED RSS TICKER CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Topbar Vertical Old Style

    private void generateTopBarV1()
    {
      StringBuilder rawXML = new StringBuilder();
      const string quote = "\"";

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Exit Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<width>340</width>");
      rawXML.AppendLine("<height>60</height>");
      rawXML.AppendLine("<posY>435</posY>");
      rawXML.AppendLine("<posX>513</posX>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<font>mediastream12tc</font>");
      rawXML.AppendLine("<label>Exit MediaPortal</label>");
      rawXML.AppendLine("<visible allowhiddenfocus=" + quote + "true" + quote + ">Control.HasFocus(7777)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Restart Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<width>340</width>");
      rawXML.AppendLine("<height>60</height>");
      rawXML.AppendLine("<posY>435</posY>");
      rawXML.AppendLine("<posX>513</posX>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<font>mediastream12tc</font>");
      rawXML.AppendLine("<label>Restart MediaPortal</label>");
      rawXML.AppendLine("<visible>Control.HasFocus(7888)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Shutdown Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<width>340</width>");
      rawXML.AppendLine("<height>60</height>");
      rawXML.AppendLine("<posY>435</posY>");
      rawXML.AppendLine("<posX>513</posX>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<font>mediastream12tc</font>");
      rawXML.AppendLine("<label>Shutdown Menu</label>");
      rawXML.AppendLine("<visible>Control.HasFocus(7999)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Shutdown menu hbar</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<posX>485</posX>");
      rawXML.AppendLine("<posY>335</posY>");
      rawXML.AppendLine("<width>381</width>");
      rawXML.AppendLine("<height>1</height>");
      rawXML.AppendLine("<texture>hbar1white.png</texture>");
      rawXML.AppendLine("<visible>Control.HasFocus(7888)|Control.HasFocus(7999)|Control.HasFocus(7777)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("\t\t\t<description>Exit Button</description>");
      rawXML.AppendLine("\t\t\t<type>button</type>");
      rawXML.AppendLine("\t\t\t<id>7777</id>");
      rawXML.AppendLine("\t\t\t<posX>493</posX>");
      rawXML.AppendLine("\t\t\t<posY>350</posY>");
      rawXML.AppendLine("\t\t\t<onleft>" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + "</onleft>");
      rawXML.AppendLine("\t\t\t<onright>7888</onright>");
      rawXML.AppendLine("\t\t\t<width>80</width>");
      rawXML.AppendLine("\t\t\t<height>80</height>");
      rawXML.AppendLine("\t\t\t<textureNoFocus>exit_button.png</textureNoFocus>");
      rawXML.AppendLine("\t\t\t<textureFocus>exit_button.png</textureFocus>");
      rawXML.AppendLine("\t\t\t<action>97</action>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
      rawXML.AppendLine("\t\t</control>");

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Restart Button</description>");
      rawXML.AppendLine("\t\t\t<type>button</type>");
      rawXML.AppendLine("\t\t\t<id>7888</id>");
      rawXML.AppendLine("\t\t\t<posX>633</posX>");
      rawXML.AppendLine("\t\t\t<posY>350</posY>");
      rawXML.AppendLine("\t\t\t<onleft>7777</onleft>");
      rawXML.AppendLine("\t\t\t<onright>7999</onright>");
      rawXML.AppendLine("\t\t\t<width>80</width>");
      rawXML.AppendLine("\t\t\t<height>80</height>");
      rawXML.AppendLine("\t\t\t<textureNoFocus>restart_button.png</textureNoFocus>");
      rawXML.AppendLine("\t\t\t<textureFocus>restart_button.png</textureFocus>");
      rawXML.AppendLine("\t\t\t<action>98</action>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
      rawXML.AppendLine("\t\t</control>");

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Shutdown Button</description>");
      rawXML.AppendLine("\t\t\t<type>button</type>");
      rawXML.AppendLine("\t\t\t<id>7999</id>");
      rawXML.AppendLine("\t\t\t<posX>773</posX>");
      rawXML.AppendLine("\t\t\t<posY>350</posY>");
      rawXML.AppendLine("\t\t\t<onleft>7888</onleft>");
      rawXML.AppendLine("\t\t\t<onright>17</onright>");
      rawXML.AppendLine("\t\t\t<width>80</width>");
      rawXML.AppendLine("\t\t\t<height>80</height>");
      rawXML.AppendLine("\t\t\t<textureNoFocus>shutdown_button.png</textureNoFocus>");
      rawXML.AppendLine("\t\t\t<textureFocus>shutdown_button.png</textureFocus>");
      rawXML.AppendLine("\t\t\t<action>99</action>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
      rawXML.AppendLine("\t\t</control>");

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Icon Fix</description>");
      rawXML.AppendLine("\t\t\t<id>0</id>");
      rawXML.AppendLine("\t\t\t<type>image</type>");
      rawXML.AppendLine("\t\t\t<posx>0</posx>");
      rawXML.AppendLine("\t\t\t<posy>0</posy>");
      rawXML.AppendLine("\t\t\t<width>1280</width>");
      rawXML.AppendLine("\t\t\t<height>720</height>");
      rawXML.AppendLine("\t\t\t<texture>tv_black.png</texture>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " start=" + quote + "200" + quote + " end=" + quote + "100" + quote + " time=" + quote + "1000" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>!Control.HasFocus(7777)+!Control.HasFocus(7888)+!Control.HasFocus(7999)</visible>");
      rawXML.AppendLine("\t\t</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED TOPBAR CODE OLD STYLE -->", rawXML.ToString());
    }

    #endregion

    #region Generate Topbar Vertical

    private void generateTopBarV()
    {
      StringBuilder rawXML = new StringBuilder();
      const string quote = "\"";
      
      string exitIsVisible = null;
      string restartIsVisible = null;
      string shutdownIsVisible = null;
      int i = 0;

      foreach (menuItem menItem in menuItems)
      {
        i++;
        String topMenuId = (menItem.id + 600).ToString();
        // Dummy control for background and menu visibilty
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Button Bar: Dummy label indicating " + menItem.name + " menu visibility</description>");
        rawXML.AppendLine("<id>" + (menItem.id + 100).ToString() + "</id>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>100</posX>");
        rawXML.AppendLine("<posY>-100</posY>");
        rawXML.AppendLine("<width>500</width>");
        rawXML.AppendLine("<height>0</height>");
        rawXML.AppendLine("<label>" + menItem.name + "</label>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("</control>");
        // group the buttons
        rawXML.AppendLine("<control><description>Topbar buttons " + menItem.name + "</description>");
        rawXML.AppendLine("<type>group</type>");
        rawXML.AppendLine("<dimColor>0x60ffffff</dimColor>");
        // Exit Button
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Exit Button: Menu Item:" + menItem.name + "</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + topMenuId + "01</id>");
        rawXML.AppendLine("<posX>150</posX>");
        rawXML.AppendLine("<posY>640</posY>");
        rawXML.AppendLine("<onleft>" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + "</onleft>");
        rawXML.AppendLine("<onright>" + topMenuId + "02" + "</onright>");
        rawXML.AppendLine("<onup>" + (menItem.id + 800).ToString() + "</onup>");
        rawXML.AppendLine("<ondown>" + (menItem.id + 700).ToString() + "</ondown>");
        rawXML.AppendLine("<width>40</width>");
        rawXML.AppendLine("<height>40</height>");
        rawXML.AppendLine("<textureNoFocus>exit_button.png</textureNoFocus>");
        rawXML.AppendLine("<textureFocus>exit_button.png</textureFocus>");
        rawXML.AppendLine("<action>97</action>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("</control>");
        if (menuItems.Count == i)
          exitIsVisible += "Control.HasFocus(" + topMenuId + "01)";
        else
          exitIsVisible += "Control.HasFocus(" + topMenuId + "01)|";
        //Restart Button
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Restart Button: Menu Item:" + menItem.name + "</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + topMenuId + "02</id>");
        rawXML.AppendLine("<posX>220</posX>");
        rawXML.AppendLine("<posY>640</posY>");
        rawXML.AppendLine("<onleft>" + topMenuId + "01" + "</onleft>");
        rawXML.AppendLine("<onright>" + topMenuId + "03" + "</onright>");
        rawXML.AppendLine("<onup>" + (menItem.id + 800).ToString() + "</onup>");
        rawXML.AppendLine("<ondown>" + (menItem.id + 700).ToString() + "</ondown>");
        rawXML.AppendLine("<width>40</width>");
        rawXML.AppendLine("<height>40</height>");
        rawXML.AppendLine("<textureNoFocus>restart_button.png</textureNoFocus>");
        rawXML.AppendLine("<textureFocus>restart_button.png</textureFocus>");
        rawXML.AppendLine("<action>98</action>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("<unfocusedAlpha>200</unfocusedAlpha>");
        rawXML.AppendLine("</control>");
        if (menuItems.Count == i)
          restartIsVisible += "Control.HasFocus(" + topMenuId + "02)";
        else
          restartIsVisible += "Control.HasFocus(" + topMenuId + "02)|";
        // Shutdown Button
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Shutdown Button: Menu Item:" + menItem.name + "</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + topMenuId + "03</id>");
        rawXML.AppendLine("<posX>290</posX>");
        rawXML.AppendLine("<posY>640</posY>");
        rawXML.AppendLine("<onleft>" + topMenuId + "02</onleft>");
        rawXML.AppendLine("<onright>" + topMenuId + "01</onright>");
        rawXML.AppendLine("<onup>" + (menItem.id + 800).ToString() + "</onup>");
        rawXML.AppendLine("<ondown>" + (menItem.id + 700).ToString() + "</ondown>");
        rawXML.AppendLine("<width>40</width>");
        rawXML.AppendLine("<height>40</height>");
        rawXML.AppendLine("<textureNoFocus>shutdown_button.png</textureNoFocus>");
        rawXML.AppendLine("<textureFocus>shutdown_button.png</textureFocus>");
        rawXML.AppendLine("<action>99</action>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("</control>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("</control>");
        if (menuItems.Count == i)
          shutdownIsVisible += "Control.HasFocus(" + topMenuId + "03)";
        else
          shutdownIsVisible += "Control.HasFocus(" + topMenuId + "03)|";
      }
      // Exit Lable
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
      rawXML.AppendLine("<visible>" + exitIsVisible + "</visible>");
      rawXML.AppendLine("</control>");
      //Restart Lable
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
      rawXML.AppendLine("<visible>" + restartIsVisible + "</visible>");
      rawXML.AppendLine("</control>");
      // Shutdown Label
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
      rawXML.AppendLine("<visible>" + shutdownIsVisible + "</visible>");
      rawXML.AppendLine("</control>");
      // Seperator Bar
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Shutdown menu hbar</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<posX>140</posX>");
      rawXML.AppendLine("<posY>630</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<height>1</height>");
      rawXML.AppendLine("<texture>hbar1white.png</texture>");
      rawXML.AppendLine("<visible>"+ exitIsVisible + "|"+ restartIsVisible + "|" + shutdownIsVisible +"</visible>");
      rawXML.AppendLine("</control>");

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

        rawXML.AppendLine("<control><description>Topbar buttons " + menItem.name + "</description>");
        rawXML.AppendLine("<type>group</type>");
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Dummy label indicating " + menItem.name + " menu visibility</description>");
        rawXML.AppendLine("<id>" + (menItem.id + 100).ToString() + "</id>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>100</posX>");
        rawXML.AppendLine("<posY>-100</posY>");
        rawXML.AppendLine("<width>500</width>");
        rawXML.AppendLine("<height>0</height>");
        rawXML.AppendLine("<label>" + menItem.name + "</label>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>490</posX>");
        rawXML.AppendLine("<posY>-5</posY>");
        rawXML.AppendLine("<height>73</height>");
        rawXML.AppendLine("<width>300</width>");
        if (useAeonGraphics.Checked)
        {
          rawXML.AppendLine("<texture>3buttonbar-a.png</texture>");
        }
        else
        {
          rawXML.AppendLine("<texture>3buttonbar.png</texture>");
        }
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>545</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<texture>restart_button.png</texture>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>615</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<texture>exit_button.png</texture>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>685</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<texture>shutdown_button.png</texture>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)</visible>");
        rawXML.AppendLine("</control>");


        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>BasicHome button button</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + topMenuId + "01</id>");
        rawXML.AppendLine("<posX>545</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<textureFocus>restart_button.png</textureFocus>");
        rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("<label>-</label>");
        rawXML.AppendLine("<action>98</action> ");
        rawXML.AppendLine("<onleft>" + topMenuId + "03</onleft>");
        rawXML.AppendLine("<onright>" + topMenuId + "02</onright>");
        rawXML.AppendLine("<onup>" + topMenuId + "01</onup>");
        rawXML.AppendLine("<ondown>" + (menItem.id + 700).ToString() + "</ondown>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Exit button</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + topMenuId + "02</id>");
        rawXML.AppendLine("<posX>615</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<textureFocus>exit_button.png</textureFocus>");
        rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("<label>-</label>");
        rawXML.AppendLine("<action>97</action>");
        rawXML.AppendLine("<onleft>" + topMenuId + "01</onleft>");
        rawXML.AppendLine("<onright>" + topMenuId + "03</onright>");
        rawXML.AppendLine("<onup>" + topMenuId + "02</onup>");
        rawXML.AppendLine("<ondown>" + (menItem.id + 700).ToString() + "</ondown>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Shutdown button</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + topMenuId + "03</id>");
        rawXML.AppendLine("<posX>685</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<textureFocus>shutdown_button.png</textureFocus>");
        rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("<label>-</label>");
        rawXML.AppendLine("<action>99</action>");
        rawXML.AppendLine("<onleft>" + topMenuId + "02</onleft>");
        rawXML.AppendLine("<onright>" + topMenuId + "01</onright>");
        rawXML.AppendLine("<onup>" + topMenuId + "03</onup>");
        rawXML.AppendLine("<ondown>" + (menItem.id + 700).ToString() + "</ondown>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("</control>");
        rawXML.AppendLine("<animation effect=\"slide\" start=\"0,-" + basicHomeValues.Button3Slide.ToString() + "\" time=\"600\" acceleration=\"-0.1\" reversible=\"false\">visiblechange</animation>");

        rawXML.AppendLine("</control> <!-- /Topbar buttons " + menItem.name + " -->");
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

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[basicHomeValues.defaultId].name + "</description>");
      rawXML.AppendLine("<type>button</type>");
      rawXML.AppendLine("<id>" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + "</id>");
      rawXML.AppendLine("<posX>0</posX>");
      rawXML.AppendLine("<posY>-30</posY>");
      rawXML.AppendLine("<label>" + menuItems[basicHomeValues.defaultId].name + "</label>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<hyperlink>" + menuItems[basicHomeValues.defaultId].hyperlink + "</hyperlink>");
      rawXML.AppendLine("<textureFocus>-</textureFocus>");
      rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
      rawXML.AppendLine("<hover>-</hover>");
      if (cbExitStyleOld.Checked)
      {
        rawXML.AppendLine("<onright>7777</onright>");
        rawXML.AppendLine("<onleft>-</onleft>");
      }
      else
      {
        rawXML.AppendLine("<onleft>" + (menuItems[basicHomeValues.defaultId].id + 600).ToString() + "01</onleft>");
        rawXML.AppendLine("<onright>" + (menuItems[basicHomeValues.defaultId].id + 600).ToString() + "01</onright>");
      }
      rawXML.AppendLine("<onup>" + (menuItems[second].id + 800).ToString() + "</onup>");
      rawXML.AppendLine("<ondown>" + (menuItems[third].id + 700).ToString() + "</ondown>");
      rawXML.AppendLine("<visible>control.isvisible(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("</control>");
      // ************ FIRST


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[first].name + "</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
      rawXML.AppendLine("<posY>142</posY>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<label>" + menuItems[first].name + "</label>");
      rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("<font>#labelFont</font>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("</control>");

      // ************** SECOND



      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[second].name + "</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
      rawXML.AppendLine("<posY>242</posY>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<label>" + menuItems[second].name + "</label>");
      rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("<font>#labelFont</font>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("</control>");

      // ******** CENTER

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[basicHomeValues.defaultId].name + "</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
      rawXML.AppendLine("<posY>342</posY>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<label>" + menuItems[basicHomeValues.defaultId].name + "</label>");
      rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
      rawXML.AppendLine("<font>#selectedFont</font>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("</control>");

      // ******** THIRD


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[third].name + "</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
      rawXML.AppendLine("<posY>442</posY>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<label>" + menuItems[third].name + "</label>");
      rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("<font>#labelFont</font>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("</control>");

      // *************** FOURTH


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[fourth].name + "</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
      rawXML.AppendLine("<posY>542</posY>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<label>" + menuItems[fourth].name + "</label>");
      rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("<font>#labelFont</font>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")|Control.HasFocus(656)</visible>");
      rawXML.AppendLine("</control>");

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

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[basicHomeValues.defaultId].name + "</description>");
      rawXML.AppendLine("<type>button</type>");
      // xxx
      rawXML.AppendLine("<id>" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + "</id>");
      rawXML.AppendLine("<posX>0</posX>");
      rawXML.AppendLine("<posY>-30</posY>");
      rawXML.AppendLine("<label>" + menuItems[basicHomeValues.defaultId].name + "</label>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<hyperlink>" + menuItems[basicHomeValues.defaultId].hyperlink + "</hyperlink>");
      rawXML.AppendLine("<textureFocus>-</textureFocus>");
      rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
      rawXML.AppendLine("<hover>-</hover>");
      rawXML.AppendLine("<onleft>" + (menuItems[second].id + 800).ToString() + "</onleft>");
      rawXML.AppendLine("<onright>" + (menuItems[third].id + 700).ToString() + "</onright>");
      rawXML.AppendLine("<onup>" + (menuItems[basicHomeValues.defaultId].id + 600).ToString() + "01</onup>");
      rawXML.AppendLine("<visible>control.isvisible(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">visiblechange</animation>");
      rawXML.AppendLine("</control>	");
      // ************ FIRST


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[first].name + "</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<posX>0</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<label>" + menuItems[first].name + "</label>");
      rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("<font>#labelFont</font>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("<animation effect=\"slide\" start=\"-160,0\" end=\"-160,0\" time=\"4\" acceleration=\"-0.0\" reversible=\"false\">WindowOpen</animation><!-- needed to display item at negative offset -->");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      // ************** SECOND



      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[second].name + "</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<posX>160</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<label>" + menuItems[second].name + "</label>");
      rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("<font>#labelFont</font>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>	");

      // ******** CENTER
      if (cbDropShadow.Checked)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>home " + menuItems[basicHomeValues.defaultId].name + "</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>481</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 1) + "</posY>");
        rawXML.AppendLine("<width>320</width>");
        rawXML.AppendLine("<height>72</height>");
        rawXML.AppendLine("<label>" + menuItems[basicHomeValues.defaultId].name + "</label>");
        rawXML.AppendLine("<textcolor>black</textcolor>");
        rawXML.AppendLine("<font>#selectedFont</font>");
        rawXML.AppendLine("<align>center</align>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
        rawXML.AppendLine("</control>	");
      }

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[basicHomeValues.defaultId].name + "</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<posX>480</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<label>" + menuItems[basicHomeValues.defaultId].name + "</label>");
      rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
      rawXML.AppendLine("<font>#selectedFont</font>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>	");

      // ******** THIRD


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[third].name + "</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<posX>800</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<label>" + menuItems[third].name + "</label>");
      rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("<font>#labelFont</font>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>	");

      // *************** FOURTH


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>home " + menuItems[fourth].name + "</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<posX>1120</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
      rawXML.AppendLine("<width>320</width>");
      rawXML.AppendLine("<height>72</height>");
      rawXML.AppendLine("<label>" + menuItems[fourth].name + "</label>");
      rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
      rawXML.AppendLine("<font>#labelFont</font>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + ")</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
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
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>" + item.name + " BACKGROUND 1</description>");
        if (item.fanartHandlerEnabled)
          rawXML.AppendLine("<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "1</id>");
        else
          rawXML.AppendLine("<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "</id>");


        if (weatherBGlink.Checked && item.isWeather && infoserviceOptions.Enabled)
        {
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>animations\\linkedweather\\#infoservice.weather.today.img.big.filenamewithoutext.jpg</texture>");
        }
        else
        {

          if (item.fanartHandlerEnabled)
          {
            rawXML.AppendLine("<type>image</type>");
            rawXML.AppendLine("<texture>#fanarthandler." + item.fanartPropery + ".backdrop1.any</texture>");

          }
          else
          {
            rawXML.AppendLine("<type>image</type>");
            rawXML.AppendLine("<texture>" + item.image + "</texture>");
          }

        }

        rawXML.AppendLine("<posx>0</posx>");
        rawXML.AppendLine("<posy>0</posy>");
        rawXML.AppendLine("<width>1280</width>");
        rawXML.AppendLine("<height>720</height>");
        if (item.fanartHandlerEnabled)
          rawXML.Append("<visible>[");
        else
          rawXML.Append("<visible>");


        for (int i = 0; i < item.ids.Count; i++)
        {
          if (i == 0)
            rawXML.Append("control.isvisible(" + item.ids[i] + ")");
          else
            rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
        }

        if (item.fanartHandlerEnabled)
          if (item.EnableMusicNowPlayingFanart)
            rawXML.Append("]+control.isvisible(91919297)+![control.isvisible(91919294)+Player.HasMedia]</visible>");
          else
            rawXML.Append("]+control.isvisible(91919297)</visible>");
        else
          rawXML.Append("</visible>");

        rawXML.AppendLine("<animation effect=\"fade\" start=\"10\" end=\"100\" time=\"1000\">VisibleChange</animation>");
        if (cbAnimateBackground.Checked)
        {
          rawXML.AppendLine("<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
        }
        rawXML.AppendLine("</control>");

        // Add second background control for random fanart provided 
        if (item.fanartHandlerEnabled)
        {
          rawXML.AppendLine("<control>");

          rawXML.AppendLine("<description>" + item.name + " BACKGROUND 2</description>");
          rawXML.AppendLine("<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "2</id>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>#fanarthandler." + item.fanartPropery + ".backdrop2.any</texture>");
          rawXML.AppendLine("<posx>0</posx>");
          rawXML.AppendLine("<posy>0</posy>");
          rawXML.AppendLine("<width>1280</width>");
          rawXML.AppendLine("<height>720</height>");
          rawXML.Append("<visible>[");

          for (int i = 0; i < item.ids.Count; i++)
          {
            if (i == 0)
              rawXML.Append("control.isvisible(" + item.ids[i] + ")");
            else
              rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
          }

          if (item.EnableMusicNowPlayingFanart)
            rawXML.Append("]+control.isvisible(91919298)+![control.isvisible(91919294)+Player.HasMedia]</visible>");
          else
            rawXML.Append("]+control.isvisible(91919298)</visible>");

          rawXML.AppendLine("<animation effect=\"fade\" start=\"10\" end=\"100\" time=\"1000\">VisibleChange</animation>");
          if (cbAnimateBackground.Checked)
          {
            rawXML.AppendLine("<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
            rawXML.AppendLine("<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          }
          rawXML.AppendLine("</control>");
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
          rawXML.AppendLine("<control>");

          rawXML.AppendLine("<description>" + item.name + " NOW PLAYING BACKGROUND 1</description>");
          rawXML.AppendLine("<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "3</id>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>#fanarthandler.music.backdrop1.play</texture>");
          rawXML.AppendLine("<posx>0</posx>");
          rawXML.AppendLine("<posy>0</posy>");
          rawXML.AppendLine("<width>1280</width>");
          rawXML.AppendLine("<height>720</height>");
          rawXML.Append("<visible>[");
          for (int i = 0; i < item.ids.Count; i++)
          {
            if (i == 0)
              rawXML.Append("control.isvisible(" + item.ids[i] + ")");
            else
              rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
          }
          rawXML.Append("]+Player.HasMedia+control.isvisible(91919295)</visible>");
          rawXML.AppendLine("<animation effect=\"fade\" start=\"10\" end=\"100\" time=\"1000\">VisibleChange</animation>");
          if (cbAnimateBackground.Checked)
          {
            rawXML.AppendLine("<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
            rawXML.AppendLine("<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          }
          rawXML.AppendLine("</control>");

          //
          // Control 2
          //
          rawXML.AppendLine("<control>");

          rawXML.AppendLine("<description>" + item.name + " NOW PLAYING BACKGROUND 2</description>");
          rawXML.AppendLine("<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "4</id>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>#fanarthandler.music.backdrop2.play</texture>");
          rawXML.AppendLine("<posx>0</posx>");
          rawXML.AppendLine("<posy>0</posy>");
          rawXML.AppendLine("<width>1280</width>");
          rawXML.AppendLine("<height>720</height>");
          rawXML.Append("<visible>[");
          for (int i = 0; i < item.ids.Count; i++)
          {
            if (i == 0)
              rawXML.Append("control.isvisible(" + item.ids[i] + ")");
            else
              rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
          }
          rawXML.Append("]+Player.HasMedia+control.isvisible(91919296)</visible>");
          rawXML.AppendLine("<animation effect=\"fade\" start=\"10\" end=\"100\" time=\"1000\">VisibleChange</animation>");
          if (cbAnimateBackground.Checked)
          {
            rawXML.AppendLine("<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
            rawXML.AppendLine("<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          }
          rawXML.AppendLine("</control>");
        }

      }
      xml = xml.Replace("<!-- BEGIN GENERATED BACKGROUND CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Context Lables Horizontal

    private void GenerateContextLabelsH()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<!-- Menu Context Labels -->");
      foreach (menuItem menItem in menuItems)
      {

        if (menItem.isDefault)
        {
          if (cbDropShadow.Checked)
          {
            // Add default label
            rawXML.AppendLine("<control>");
            rawXML.AppendLine("<description>" + menItem.name + " Label (Default)</description>");
            rawXML.AppendLine("<type>label</type>");
            rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - 4) + "</posY>");
            rawXML.AppendLine("<posX>481</posX>");
            rawXML.AppendLine("<width>320</width>");
            rawXML.AppendLine("<height>24</height>");
            rawXML.AppendLine("<label>" + menItem.contextLabel + "</label>");
            rawXML.AppendLine("<textcolor>" + dropShadowColor + "</textcolor>");
            rawXML.AppendLine("<font>mediastream14tc</font>");
            rawXML.AppendLine("<align>center</align>");
            rawXML.AppendLine("<animation effect=\"fade\" delay=\"400\" time=\"300\">Visible</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 900).ToString() + ")</visible>");
            rawXML.AppendLine("</control>");
          }
          // Add default label
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + menItem.name + " Label (Default)</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - 5) + "</posY>");
          rawXML.AppendLine("<posX>480</posX>");
          rawXML.AppendLine("<width>320</width>");
          rawXML.AppendLine("<height>24</height>");
          rawXML.AppendLine("<label>" + menItem.contextLabel + "</label>");
          rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
          rawXML.AppendLine("<font>mediastream14tc</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<animation effect=\"fade\" delay=\"400\" time=\"300\">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 900).ToString() + ")</visible>");
          rawXML.AppendLine("</control>");
        }
        if (cbDropShadow.Checked)
        {
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + menItem.name + " Label</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - 4) + "</posY>");
          rawXML.AppendLine("<posX>481</posX>");
          rawXML.AppendLine("<width>320</width>");
          rawXML.AppendLine("<height>24</height>");
          rawXML.AppendLine("<label>" + menItem.contextLabel + "</label>");
          rawXML.AppendLine("<textcolor>" + dropShadowColor + "</textcolor>");
          rawXML.AppendLine("<font>mediastream14tc</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
          rawXML.AppendLine("</control>");
        }
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>" + menItem.name + " Label</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - 5) + "</posY>");
        rawXML.AppendLine("<posX>480</posX>");
        rawXML.AppendLine("<width>320</width>");
        rawXML.AppendLine("<height>24</height>");
        rawXML.AppendLine("<label>" + menItem.contextLabel + "</label>");
        rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
        rawXML.AppendLine("<font>mediastream14tc</font>");
        rawXML.AppendLine("<align>center</align>");
        rawXML.AppendLine("<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
        rawXML.AppendLine("</control>");
      }

      xml = xml.Replace("<!-- BEGIN CONTEXT LABELS CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Context Labels Vertical

    private void GenerateContextLabelsV()
    {
      StringBuilder rawXML = new StringBuilder();
      const string quote = "\"";

      rawXML.AppendLine("<!-- Menu Context Labels -->");
      foreach (menuItem menItem in menuItems)
      {
        if (menItem.isDefault)
        {
          // Add default label
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + menItem.name + " Label (Default)</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
          rawXML.AppendLine("<posY>322</posY>");
          rawXML.AppendLine("<width>380</width>");
          rawXML.AppendLine("<height>72</height>");
          rawXML.AppendLine("<label>" + menItem.contextLabel + "</label>");
          rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
          rawXML.AppendLine("<font>#labelFont</font>");
          rawXML.AppendLine("<align>right</align>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
          rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 900).ToString() + ")</visible>");
          rawXML.AppendLine("</control>");
        }

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>" + menItem.name + " Label</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
        rawXML.AppendLine("<posY>322</posY>");
        rawXML.AppendLine("<width>380</width>");
        rawXML.AppendLine("<height>72</height>");
        rawXML.AppendLine("<label>" + menItem.contextLabel + "</label>");
        rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
        rawXML.AppendLine("<font>#labelFont</font>");
        rawXML.AppendLine("<align>right</align>");
        rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " delay=" + quote + "350" + quote + " time=" + quote + "250" + quote + ">Visible</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")</visible>");
        rawXML.AppendLine("</control>");

        // Display the current weather location under the menu option
        if (menItem.isWeather)
        {
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + menItem.name + " Label</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>" + (int.Parse(txtMenuPos.Text) + textXOffset) + "</posX>");
          rawXML.AppendLine("<posY>395</posY>");
          rawXML.AppendLine("<width>380</width>");
          rawXML.AppendLine("<height>62</height>");
          rawXML.AppendLine("<label> in #infoservice.weather.location</label>");
          rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
          rawXML.AppendLine("<font>mediastream10tc</font>");
          rawXML.AppendLine("<align>right</align>");
          rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " delay=" + quote + "350" + quote + " time=" + quote + "250" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")]</visible>");
          rawXML.AppendLine("</control>");
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
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>5-Day Weather Dummy Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>6767</id>");
      rawXML.AppendLine("<posX>-50</posX>");
      rawXML.AppendLine("<posY>-50</posY>");
      rawXML.AppendLine("<label></label>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: Forecast BGs</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      rawXML.AppendLine("<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");


      // ******************************** Weather Backgrounds **********************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>TODAY BG</description>");
      rawXML.AppendLine("<posX>" + (xPos1 - 200).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 155).ToString() + "</posY>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>6777</id>");
      rawXML.AppendLine("<width>180</width>");
      rawXML.AppendLine("<height>270</height>");
      rawXML.AppendLine("<texture>weather2.png</texture>");
      rawXML.AppendLine("</control>");
      for (i = 1; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " BG</description>");
        if (i < 3)
        {
          rawXML.AppendLine("<posX>" + (xPos2 + (spacing * i)).ToString() + "</posX>");
          rawXML.AppendLine("<posY>" + yPos1.ToString() + "</posY>");
        }
        else
        {
          rawXML.AppendLine("<posX>" + (xPos2 + (spacing * (i - 2))).ToString() + "</posX>");
          rawXML.AppendLine("<posY>" + yPos2.ToString() + "</posY>");
        }
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>6777</id>");
        rawXML.AppendLine("<width>180</width>");
        rawXML.AppendLine("<height>270</height>");
        rawXML.AppendLine("<texture>weather2.png</texture>");
        rawXML.AppendLine("</control>");
      }
      rawXML.AppendLine("</control>");

      // ********************************* Weather Icons **************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>TODAY ICON</description>");
      rawXML.AppendLine("<id>0</id>");
      if (WeatherIconsAnimated.Checked)
      {
        rawXML.AppendLine("<type>multiimage</type>");
        rawXML.AppendLine("<imagepath>" + weatherIcon(0) + "</imagepath>");
        rawXML.AppendLine("<timeperimage>33</timeperimage>");
        rawXML.AppendLine("<loop>True</loop>");
      }
      else
      {
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<texture>" + weatherIcon(0) + "</texture>");
      }
      rawXML.AppendLine("<posX>" + (xPos1 - 200).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + ((yPos1 + 155) - 70).ToString() + "</posY>");
      rawXML.AppendLine("<height>180</height>");
      rawXML.AppendLine("<width>180</width>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");

      rawXML.AppendLine("<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      rawXML.AppendLine("</control>");

      for (i = 1; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " ICON</description>");
        rawXML.AppendLine("<id>0</id>");
        if (WeatherIconsAnimated.Checked)
        {
          rawXML.AppendLine("<type>multiimage</type>");
          rawXML.AppendLine("<imagepath>" + weatherIcon(i) + "</imagepath>");
          rawXML.AppendLine("<timeperimage>33</timeperimage>");
          rawXML.AppendLine("<loop>True</loop>");
        }
        else
        {
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>" + weatherIcon(i) + "</texture>");
        }
        if (i < 3)
        {
          rawXML.AppendLine("<posX>" + (xPos2 + (spacing * i)).ToString() + "</posX>");
          rawXML.AppendLine("<posY>" + (yPos1 - 70).ToString() + "</posY>");
        }
        else
        {
          rawXML.AppendLine("<posX>" + (xPos2 + (spacing * (i - 2))).ToString() + "</posX>");
          rawXML.AppendLine("<posY>" + (yPos2 - 70).ToString() + "</posY>");
        }
        rawXML.AppendLine("<height>180</height>");
        rawXML.AppendLine("<width>180</width>");
        if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
        else
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
        rawXML.AppendLine("<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
        rawXML.AppendLine("</control>");
      }
      // ************************************* Weather Text Items *******************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FULL WEATHER DETAILS</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");

      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      // ************************************* Day 1 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + ((xPos1 - 200) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 245 + 155).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>6030</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 TEMP</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + ((xPos1 - 200) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 180 + 155).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("<font>mediastream28tc</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      if (getInfoServiceVersion().CompareTo("1.6.0.0") < 0)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY 1 MAX VALUE</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>" + ((xPos1 - 200) + 5).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + (yPos1 + 200 + 155).ToString() + "</posY>");
        rawXML.AppendLine("<align>left</align>");
        rawXML.AppendLine("<label>#infoservice.weather.today.maxtemp</label>");
        rawXML.AppendLine("<font>mediastream12</font>");
        rawXML.AppendLine("<textcolor>white</textcolor>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY 1 MIN VALUE</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>" + ((xPos1 - 200) + 175).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + (yPos1 + 200 + 155).ToString() + "</posY>");
        rawXML.AppendLine("<align>right</align>");
        rawXML.AppendLine("<label>#infoservice.weather.today.mintemp</label>");
        rawXML.AppendLine("<font>mediastream12</font>");
        rawXML.AppendLine("<textcolor>white</textcolor>");
        rawXML.AppendLine("</control>");
      }

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 GENERAL WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + ((xPos1 - 200) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 100 + 155).ToString() + "</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<height>60</height>");
      rawXML.AppendLine("<font>mediastream13</font>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 2 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 245).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream12</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 175).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.mintemp</label>");
      rawXML.AppendLine("<font>mediastream12</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 AM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 100).ToString() + "</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 PM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 150).ToString() + "</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 3 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 245).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 175).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 100).ToString() + "</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 150).ToString() + "</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 4 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 245).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 175).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 100).ToString() + "</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 150).ToString() + "</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 5 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 245).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 175).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 100).ToString() + "</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 150).ToString() + "</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
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
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>5-Day Weather Dummy Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>6767</id>");
      rawXML.AppendLine("<posX>-50</posX>");
      rawXML.AppendLine("<posY>-50</posY>");
      rawXML.AppendLine("<label></label>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      rawXML.AppendLine("</control>");

      // Group for Weather Backgrounds
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FIVE DAY WEATHER BACKGROUNDS</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      rawXML.AppendLine("<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " BG</description>");
        rawXML.AppendLine("<posX>" + (xPos1 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + yPos1.ToString() + "</posY>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<width>200</width>");
        rawXML.AppendLine("<height>170</height>");
        rawXML.AppendLine("<texture>weather2.png</texture>");
        rawXML.AppendLine("</control>");

      }
      rawXML.AppendLine("</control>");

      //Group for weather Icons
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FIVE DAY WEATHER ICONS</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      // ********************* Weather Icons **************************************
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " ICON</description>");
        rawXML.AppendLine("<id>0</id>");
        if (WeatherIconsAnimated.Checked)
        {
          rawXML.AppendLine("<type>multiimage</type>");
          rawXML.AppendLine("<imagepath>" + weatherIcon(i) + "</imagepath>");
          rawXML.AppendLine("<timeperimage>33</timeperimage>");
          rawXML.AppendLine("<loop>True</loop>");
        }
        else
        {
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>" + weatherIcon(i) + "</texture>");
        }
        rawXML.AppendLine("<posX>" + (xPos1 + 20 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + (yPos1 - 4).ToString() + "</posY>");
        rawXML.AppendLine("<height>80</height>");
        rawXML.AppendLine("<width>80</width>");
        if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")]</visible>");
        else
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
        rawXML.AppendLine("<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
        rawXML.AppendLine("</control>");

        // This control was for debugging only, it displays the weather condition number
        //rawXML.AppendLine("<control>");
        //rawXML.AppendLine("<description>DAY 1 LABEL</description>");
        //rawXML.AppendLine("<type>label</type>");
        //rawXML.AppendLine("<id>0</id>");
        //rawXML.AppendLine("<posX>100</posX>");
        //rawXML.AppendLine("<posY>" + (200 + (30 * i)).ToString() + "</posY>");
        //rawXML.AppendLine("<width>800</width>");
        //rawXML.AppendLine("<align>center</align>");
        //rawXML.AppendLine("<label>" + weatherIcon(i) + "</label>");
        //rawXML.AppendLine("<font>mediastream11tc</font>");
        //rawXML.AppendLine("<textcolor>White</textcolor>");
        //rawXML.AppendLine("</control>");

      }
      rawXML.AppendLine("</control>");



      //Group for weather Text
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FIVE DAY WEATHER TEXT</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")]</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      // **************************************** DAY 1 *********************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>120</posX>");
      rawXML.AppendLine("<posY>699</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>6030</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 TEMP</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>300</posX>");
      rawXML.AppendLine("<posY>615</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      if (getInfoServiceVersion().CompareTo("1.6.0.0") < 0)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY 1 MAX VALUE</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>300</posX>");
        rawXML.AppendLine("<posY>559</posY>");
        rawXML.AppendLine("<align>right</align>");
        rawXML.AppendLine("<label>#infoservice.weather.today.maxtemp</label>");
        rawXML.AppendLine("<font>mediastream11</font>");
        rawXML.AppendLine("<textcolor>white</textcolor>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY 1 MIN VALUE</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>300</posX>");
        rawXML.AppendLine("<posY>584</posY>");
        rawXML.AppendLine("<align>right</align>");
        rawXML.AppendLine("<label>#infoservice.weather.today.mintemp</label>");
        rawXML.AppendLine("<font>mediastream11</font>");
        rawXML.AppendLine("<textcolor>white</textcolor>");
        rawXML.AppendLine("</control>");
      }

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 GENERAL WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>140</posX>");
      rawXML.AppendLine("<posY>634</posY>");
      rawXML.AppendLine("<width>140</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("</control>");
      // **************************************** DAY 2 *****************************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>330</posX>");
      rawXML.AppendLine("<posY>699</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>510</posX>");
      rawXML.AppendLine("<posY>559</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>510</posX>");
      rawXML.AppendLine("<posY>584</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>350</posX>");
      rawXML.AppendLine("<posY>634</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>350</posX>");
      rawXML.AppendLine("<posY>654</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // **************************************** DAY 3 ***********************************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>540</posX>");
      rawXML.AppendLine("<posY>699</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>720</posX>");
      rawXML.AppendLine("<posY>559</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>720</posX>");
      rawXML.AppendLine("<posY>584</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>560</posX>");
      rawXML.AppendLine("<posY>634</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>560</posX>");
      rawXML.AppendLine("<posY>654</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // **************************************** DAY 4 ***********************************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>750</posX>");
      rawXML.AppendLine("<posY>699</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>930</posX>");
      rawXML.AppendLine("<posY>559</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>930</posX>");
      rawXML.AppendLine("<posY>584</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>770</posX>");
      rawXML.AppendLine("<posY>634</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>770</posX>");
      rawXML.AppendLine("<posY>654</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // **************************************** DAY 5 ***********************************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>960</posX>");
      rawXML.AppendLine("<posY>699</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1140</posX>");
      rawXML.AppendLine("<posY>559</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1140</posX>");
      rawXML.AppendLine("<posY>584</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>980</posX>");
      rawXML.AppendLine("<posY>634</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>980</posX>");
      rawXML.AppendLine("<posY>654</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
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
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: Forecast BGs</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      rawXML.AppendLine("<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " BG</description>");
        rawXML.AppendLine("<posX>" + (xPos1 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + yPos1.ToString() + "</posY>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<width>180</width>");
        rawXML.AppendLine("<height>270</height>");
        rawXML.AppendLine("<texture>weather2.png</texture>");
        rawXML.AppendLine("</control>");
      }
      rawXML.AppendLine("</control>");

      //Group for weather Icons
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FULL WEATHER ICONS (Animated)</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      // ********************* Weather Icons **************************************
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " ICON</description>");
        rawXML.AppendLine("<id>0</id>");
        if (WeatherIconsAnimated.Checked)
        {
          rawXML.AppendLine("<type>multiimage</type>");
          rawXML.AppendLine("<imagepath>" + weatherIcon(i) + "</imagepath>");
          rawXML.AppendLine("<timeperimage>33</timeperimage>");
          rawXML.AppendLine("<loop>True</loop>");
        }
        else
        {
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>" + weatherIcon(i) + "</texture>");
        }
        rawXML.AppendLine("<posX>" + (xPos1 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + (yPos1 - 70).ToString() + "</posY>");
        rawXML.AppendLine("<height>180</height>");
        rawXML.AppendLine("<width>180</width>");
        if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
        else
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
        rawXML.AppendLine("<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
        rawXML.AppendLine("</control>");
      }
      rawXML.AppendLine("</control>");
      // Group for all text parts
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FULL WEATHER</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<animation effect=\"fade\" delay=\"1000\" time=\"300\" tween=\"linear\">Visible</animation>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]</visible>");
      // ************************************* Day 1 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>210</posX>");
      rawXML.AppendLine("<posY>394</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>6030</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 TEMP</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>210</posX>");
      rawXML.AppendLine("<posY>310</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("<font>mediastream28tc</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      if (getInfoServiceVersion().CompareTo("1.6.0.0") < 0)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY 1 MAX VALUE</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>125</posX>");
        rawXML.AppendLine("<posY>355</posY>");
        rawXML.AppendLine("<align>left</align>");
        rawXML.AppendLine("<label>#infoservice.weather.today.maxtemp</label>");
        rawXML.AppendLine("<font>mediastream12</font>");
        rawXML.AppendLine("<textcolor>white</textcolor>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY 1 MIN VALUE</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>295</posX>");
        rawXML.AppendLine("<posY>355</posY>");
        rawXML.AppendLine("<align>right</align>");
        rawXML.AppendLine("<label>#infoservice.weather.today.mintemp</label>");
        rawXML.AppendLine("<font>mediastream12</font>");
        rawXML.AppendLine("<textcolor>white</textcolor>");
        rawXML.AppendLine("</control>");
      }

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 GENERAL WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>125</posX>");
      rawXML.AppendLine("<posY>230</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<height>60</height>");
      rawXML.AppendLine("<font>mediastream13</font>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 2 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>420</posX>");
      rawXML.AppendLine("<posY>394</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>335</posX>");
      rawXML.AppendLine("<posY>355</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream12</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>505</posX>");
      rawXML.AppendLine("<posY>355</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.mintemp</label>");
      rawXML.AppendLine("<font>mediastream12</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 AM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>340</posX>");
      rawXML.AppendLine("<posY>230</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 PM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>340</posX>");
      rawXML.AppendLine("<posY>285</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 3 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>630</posX>");
      rawXML.AppendLine("<posY>394</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>545</posX>");
      rawXML.AppendLine("<posY>355</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>715</posX>");
      rawXML.AppendLine("<posY>355</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>545</posX>");
      rawXML.AppendLine("<posY>230</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>545</posX>");
      rawXML.AppendLine("<posY>285</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 4 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>840</posX>");
      rawXML.AppendLine("<posY>394</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>755</posX>");
      rawXML.AppendLine("<posY>355</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>925</posX>");
      rawXML.AppendLine("<posY>355</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>755</posX>");
      rawXML.AppendLine("<posY>230</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>755</posX>");
      rawXML.AppendLine("<posY>285</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 5 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1050</posX>");
      rawXML.AppendLine("<posY>394</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>965</posX>");
      rawXML.AppendLine("<posY>355</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1135</posX>");
      rawXML.AppendLine("<posY>355</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>965</posX>");
      rawXML.AppendLine("<posY>230</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>965</posX>");
      rawXML.AppendLine("<posY>285</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED FIVE DAY WEATHER CODE -->", rawXML.ToString());
    }

    #endregion

    #region Generate Weather Summary

    private void generateWeathersummary(int weatherId)
    {

      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: WEATHER SUMMARY</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+!control.isvisible(" + int.Parse(weatherId.ToString()) + ")</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 0 BG</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>976</posX>");
      rawXML.AppendLine("<posY>-3</posY>");
      rawXML.AppendLine("<width>306</width>");
      rawXML.AppendLine("<height>75</height>");
      rawXML.AppendLine("<texture>homeweatheroverlaybg.png</texture>");
      rawXML.AppendLine("</control>");

      if (WeatherIconsAnimated.Checked)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Todays weather image (Animated Version)</description>");
        rawXML.AppendLine("<type>multiimage</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>993</posX>");
        rawXML.AppendLine("<posY>9</posY>");
        rawXML.AppendLine("<height>54</height>");
        rawXML.AppendLine("<width>54</width>");
        rawXML.AppendLine("<centered>no</centered>");
        rawXML.AppendLine("<texture>-</texture>");
        rawXML.AppendLine("<imagepath>" + weatherIcon(0) + "</imagepath>");
        rawXML.AppendLine("<timeperimage>33</timeperimage>");
        rawXML.AppendLine("<loop>True</loop>");
        rawXML.AppendLine("</control>");
      }
      else
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Todays weather image (Animated Version)</description>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>993</posX>");
        rawXML.AppendLine("<posY>9</posY>");
        rawXML.AppendLine("<height>54</height>");
        rawXML.AppendLine("<width>54</width>");
        rawXML.AppendLine("<centered>no</centered>");
        rawXML.AppendLine("<texture>" + weatherIcon(0) + "</texture>");
        rawXML.AppendLine("</control>");

      }
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Temperature</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>400</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<posX>1265</posX>");
      rawXML.AppendLine("<posY>30</posY>");
      rawXML.AppendLine("<font>mediastream14c</font>");
      rawXML.AppendLine("<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Todays Weather Condition</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>150</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<posX>1055</posX>");
      rawXML.AppendLine("<posY>15</posY>");
      rawXML.AppendLine("<font>mediastream10tc</font>");
      rawXML.AppendLine("<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("</control>");

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

    #region Add Fanart Controls Import

    private void generateFanartControls()
    {
      StringBuilder rawXML = new StringBuilder();
      rawXML.AppendLine("<import>common.fanartcontrols.basichome.xml</import>");
      xml = xml.Replace("<!-- BEGIN FANART HANDLER CONTROLS -->", rawXML.ToString());
    }

    #endregion

    #region Background Loading

    private void generateBackgroundLoading()
    {
      StringBuilder rawXML = new StringBuilder();
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>BACKGROUND LOADING</description>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<posX>0</posX>");
      rawXML.AppendLine("<posY>0</posY>");
      rawXML.AppendLine("<width>1280</width>");
      rawXML.AppendLine("<height>720</height>");
      rawXML.AppendLine("<texture>LoadingBackdrop.png</texture>");
      rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "0" + quote + " time=" + quote + "10" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN BACKGROUND LOADING -->", rawXML.ToString());
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
            if (mostRecentStyle == mostRecentSummaryStyle.poster)
              rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.VSum.xml</import>");
            else
              rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.VSum2.xml</import>");

        }
        else
        {
          if (tvSeriesRecentStyle == tvSeriesRecentType.full)
            rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.HFull.xml</import>");
          else
            if (mostRecentStyle == mostRecentSummaryStyle.poster)
            rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.HSum.xml</import>");
            else
              rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.HSum2.xml</import>");
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


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Menu Twitter Sub Menu</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>99004</id>");
      rawXML.AppendLine("<posX>280</posX>");
      rawXML.AppendLine("<posY>400</posY>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter).ToString() + "</posY>");
      rawXML.AppendLine("<width>1000</width>");
      rawXML.AppendLine("<height>" + basicHomeValues.subMenuTopHeight + "</height>");
      rawXML.AppendLine("<texture>" + basicHomeValues.mymenu_submenutop + "</texture>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      if (wrapString.Checked)
      {
        if (useInfoServiceSeparator)
          //rawXML.AppendLine("<wrapString> #infoservice.feed.separator </wrapString>");
          rawXML.AppendLine("<wrapString> :: </wrapString>");
        else
          rawXML.AppendLine("<wrapString> :: </wrapString>");
      }

      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,320" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Twitter image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<width>28</width>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - (basicHomeValues.offsetTwitterImage - 5)).ToString() + "</posY>");
      rawXML.AppendLine("<posX>330</posX>");
      rawXML.AppendLine("<texture>InfoService\\defaultTwitter.png</texture>");
      rawXML.AppendLine("<keepaspectratio>yes</keepaspectratio>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,320" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Twitter Items</description>");
      rawXML.AppendLine("<type>fadelabel</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>1160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - (basicHomeValues.offsetTwitterImage - 6)).ToString() + "</posY>");
      rawXML.AppendLine("<posX>360</posX>");
      rawXML.AppendLine("<font>mediastream12</font>");
      rawXML.AppendLine("<textcolor>ff000000</textcolor>");
      rawXML.AppendLine("<label>#infoservice.twitter.messages</label>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,320" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED TWITTER CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Twitter Vertical

    private void generateTwitterV()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Menu Twitter Sub Menu</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>99004</id>");
      rawXML.AppendLine("<posX>140</posX>");
      rawXML.AppendLine("<posY>9</posY>");
      rawXML.AppendLine("<width>840</width>");
      rawXML.AppendLine("<height>34</height>");
      rawXML.AppendLine("<texture>" + basicHomeValues.mymenu_submenutop + "</texture>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      if (wrapString.Checked)
      {
        if (useInfoServiceSeparator)
          //rawXML.AppendLine("<wrapString> #infoservice.feed.separator </wrapString>");
          rawXML.AppendLine("<wrapString> :: </wrapString>");
        else
          rawXML.AppendLine("<wrapString> :: </wrapString>");
      }

      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Twitter image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>18</id>");
      rawXML.AppendLine("<width>20</width>");
      rawXML.AppendLine("<posY>16</posY>");
      rawXML.AppendLine("<posX>149</posX>");
      rawXML.AppendLine("<texture>InfoService\\defaultTwitter.png</texture>");
      rawXML.AppendLine("<keepaspectratio>yes</keepaspectratio>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Twitter Items</description>");
      rawXML.AppendLine("<type>fadelabel</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>775</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<posY>15</posY>");
      rawXML.AppendLine("<posX>175</posX>");
      rawXML.AppendLine("<font>mediastream12tc</font>");
      rawXML.AppendLine("<label>#infoservice.twitter.messages</label>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
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
      string mostRecentSumStyle = "fanart";

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
      string mostRecentCycleFanart = cbCycleFanart.Checked ? "true" : "false";
      string mrSeriesEpisodeFormat = mrsForm.mrSeriesEpisodeFormat ? "true" : "false";
      string mrTitleLast = mrsForm.mrTitleLast ? "true" : "false";
      string settingOldStyleExitButtons = cbExitStyleOld.Checked ? "true" : "false";





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

      if (rbFanartStyle.Checked)
        mostRecentSumStyle = "fanart";
      else
        mostRecentSumStyle = "poster";

      xml = ("<profile>"
                + "<version>1.0</version>"
                + "<skin name=\"StreamedMP\">"
                + "<section name=" + quote + "StreamedMP Options" + quote + ">"
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
                + generateEntry("mostRecentSumStyle", mostRecentSumStyle, 2, true)
                + generateEntry("mostRecentCycleFanart", mostRecentCycleFanart, 2, true)
                + generateEntry("mrSeriesEpisodeFormat", mrSeriesEpisodeFormat, 2, true)
                + generateEntry("mrTitleLast", mrTitleLast, 2, true)
                + generateEntry("mrEpisodeFont", mrsForm.mrEpisodeFont, 2, true)
                + generateEntry("mrSeriesFont", mrsForm.mrSeriesFont, 2, true)
                + generateEntry("settingOldStyleExitButtons", settingOldStyleExitButtons,2,true)
                + "</section>");
      
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<!-- End Of Menu Options --><section name=" + quote + "StreamedMP Menu Items" + quote + ">");

      int menuIndex = 0;
      rawXML.AppendLine(generateEntry("count", menuItems.Count.ToString(), 2, false));
      foreach (menuItem menItem in menuItems)
      {
        rawXML.AppendLine("<!-- Menu Entry : " + menuIndex.ToString() + " -->");
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

        if (menItem.fanartHandlerEnabled)
          fanartHandlerUsed = true;
      }
      rawXML.AppendLine("</section>");
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