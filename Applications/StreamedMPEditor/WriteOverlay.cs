using SMPCheckSum;
using System.IO;

namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {
    private void generateOverlay(int baseYpos, int baseXpos, int weatherId)
    {
      int overlayYpos = 0;
      int endYpos = 0;
      int maxEndYpos = 540;
      int overlayOffset = 140;
      string overlayFanart = "No";
      string hideControls = "!control.isvisible(" + int.Parse(weatherId.ToString()) + ")";

      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
      {
        hideControls += "+![";
        foreach (menuItem menItem in menuItems)
        {
          hideControls += "control.isvisible(" + (menItem.id + 100).ToString() + ")|";
        }
        hideControls = hideControls.Substring(0, hideControls.Length - 1) + "]";
      }

      if (menuStyle == chosenMenuStyle.horizontalContextStyle || enableFiveDayWeather.Checked == false)
        hideControls = "";

      if (cbMostRecentTvSeries.Checked && (rbTBSeriesFull.Checked || mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.poster))
        if (hideControls == "")
          hideControls += "!control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ")";
        else
          hideControls += "+!control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ")";

      if (cbMostRecentMovPics.Checked && (rbMovPicsFull.Checked || mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.poster))
        if (hideControls == "")
          hideControls += "!control.isvisible(" + basicHomeValues.movingPicturesControl.ToString() + ")";
        else
          hideControls += "+!control.isvisible(" + basicHomeValues.movingPicturesControl.ToString() + ")";

      if (horizontalContextLabels.Checked)
      {
        maxEndYpos = 530;
        overlayOffset = 158;
      }

      if (enableTwitter.Checked)
        overlayOffset += (basicHomeValues.subMenuTopHeight / 2);

      endYpos = (baseYpos + basicHomeValues.offsetMymenu) + (basicHomeValues.menuHeight / 2);

      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
        endYpos += (basicHomeValues.subMenuHeight / 2);

      if (endYpos > maxEndYpos)
        overlayYpos = baseYpos - overlayOffset;
      else
        overlayYpos = 568;

      if (menuStyle == chosenMenuStyle.verticalStyle)
        overlayYpos = 540;

      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        overlayYpos = baseYpos;

      if (cbOverlayFanart.Checked)
        overlayFanart = "Yes";

      if (hideControls.Length > 0)
      {
        if (!hideControls.StartsWith("+"))
          hideControls = "+" + hideControls;
      }

      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + " standalone=" + quote + "yes" + quote + "?>" +
            "<!--" +
            "different overlays on home screens" +
            "-->" +
            "<window>" +
              "<controls>" +
              "<!--                                    :: DUMMY / BACKGROUND ::                                    -->" +
                "<control>" +
                "<description>dummy (To enable fanart set this control to visible)</description>" +
                  "<type>label</type>" +
                  "<id>3339</id>" +
                  "<posX>2000</posX>" +
                  "<label></label>" +
                  "<visible>" + overlayFanart + "</visible>" +
                "</control>" +
                "<control>" +
                  "<description>DUMMY CONTROL FOR FANART 1 RANDOM VISIBILITY CONDITION</description>" +
                  "<type>label</type>" +
                  "<id>91919297</id>" +
                  "<posX>0</posX>" +
                  "<posY>0</posY>" +
                  "<width>1</width>" +
                "</control>" +
                "<control>" +
                  "<description>DUMMY CONTROL FOR FANART 2 RANDOM VISIBILITY CONDITION</description>" +
                  "<type>label</type>" +
                  "<id>91919298</id>" +
                  "<posX>0</posX>" +
                  "<posY>0</posY>" +
                  "<width>1</width>" +
                "</control>" +
                "<control>" +
                  "<description>DUMMY CONTROL FOR FANART 1 PLAY VISIBILITY CONDITION</description>" +
                  "<type>label</type>" +
                  "<id>91919295</id>" +
                  "<posX>0</posX>" +
                  "<posY>0</posY>" +
                  "<width>1</width>" +
                "</control>" +
                "<control>" +
                  "<description>DUMMY CONTROL FOR FANART 2 PLAY VISIBILITY CONDITION</description>" +
                  "<type>label</type>" +
                  "<id>91919296</id>" +
                  "<posX>0</posX>" +
                  "<posY>0</posY>" +
                  "<width>1</width>" +
                "</control>" +
                "<control>" +
                "<description>DUMMY CONTROL FOR FANART AVAILABILITY CONDITION</description>" +
                  "<type>label</type>" +
                  "<id>91919294</id>" +
                  "<posX>0</posX>" +
                  "<posY>0</posY>" +
                  "<width>1</width>" +
                  "<visible>no</visible>" +
                "</control>" +
                "<control>" +
                  "<description>dummy (visible when music is playing)</description>" +
                  "<type>label</type>" +
                  "<id>3337</id>" +
                  "<posX>2000</posX>" +
                  "<label>#Play.Current.Album</label>" +
                  "<visible>!player.hasvideo+player.hasaudio</visible>" +
                "</control>" +
                "<control>" +
                  "<description>dummy (visible when there is a next track)</description>" +
                  "<type>label</type>" +
                  "<id>3338</id>" +
                  "<posX>2000</posX>" +
                  "<label>#Play.Next.Title</label>" +
                  "<visible>player.hasmedia+control.hastext(3338)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>dialog bg</description>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<posX>" + baseXpos.ToString() + "</posX>" +
                  "<posY>" + overlayYpos.ToString() + "</posY>" +
                  "<width>519</width>" +
                  "<height>152</height>" +
                  "<texture>dialogprogressbg.png</texture>" +
                  "<visible>player.hasmedia+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                  "<!-- Not visible in regular home and my plugins -->" +
                "</control>" +
                "<control>" +
                  "<description>PICTURES NOW PLAYING BACKGROUND 1</description>" +
                  "<id>0</id>" +
                  "<type>image</type>" +
                  "<posX>" + (baseXpos + 225).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 18).ToString() + "</posY>" +
                  "<width>270</width>" +
                  "<height>114</height>" +
                  "<texture>#fanarthandler.music.backdrop1.play</texture>" +
                  "<visible>control.isvisible(3339)+Player.HasMedia+control.isvisible(91919295)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "10" + quote + " end=" + quote + "100" + quote + " time=" + quote + "1000" + quote + ">Visible</animation>" +
                "</control>" +
                "<control>" +
                  "<description>PICTURES NOW PLAYING BACKGROUND 2</description>" +
                  "<id>0</id>" +
                  "<type>image</type>" +
                  "<posX>" + (baseXpos + 225).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 18).ToString() + "</posY>" +
                  "<width>270</width>" +
                  "<height>114</height>" +
                  "<texture>#fanarthandler.music.backdrop2.play</texture>" +
                  "<visible>control.isvisible(3339)+Player.HasMedia+control.isvisible(91919296)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "10" + quote + " end=" + quote + "100" + quote + " time=" + quote + "1000" + quote + ">Visible</animation>" +
                "</control>" +
                "<control>" +
                  "<description>Overlay Mask</description>" +
                  "<id>0</id>" +
                  "<type>image</type>" +
                  "<posX>" + (baseXpos + 18).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 18).ToString() + "</posY>" +
                  "<width>512</width>" +
                  "<height>114</height>" +
                  "<colordiffuse>EFFFFFFF</colordiffuse>" +
                  "<texture>overlayfanartmask.png</texture>" +
                  "<visible>control.isvisible(3339)+Player.HasMedia+control.isvisible(91919294)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<!--                                    :: MUSIC OVERLAY IN BASIC HOME ::                                    -->" +
                "<control>" +
                  "<description>music logo</description>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXpos + 18).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 18).ToString() + "</posY>" +
                  "<width>114</width>" +
                  "<height>114</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<centered>yes</centered>" +
                  "<zoom>no</zoom>" +
                  "<texture>defaultAudioBig.png</texture>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+string.starts(#Play.Current.thumb,#)+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>music logo</description>" +
                  "<type>image</type>" +
                  "<id>7220</id>" +
                  "<posX>" + (baseXpos + 18).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 18).ToString() + "</posY>" +
                  "<width>114</width>" +
                  "<height>114</height>" +
                  "<keepaspectratio>no</keepaspectratio>" +
                  "<centered>yes</centered>" +
                  "<zoom>yes</zoom>" +
                  "<texture>#StreamedMP.ArtistPath\\#Play.Current.ArtistL.jpg</texture>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+string.equals(#Play.Current.thumb,)+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>music logo</description>" +
                  "<type>image</type>" +
                  "<id>7230</id>" +
                  "<posX>" + (baseXpos + 18).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 18).ToString() + "</posY>" +
                  "<width>114</width>" +
                  "<height>114</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<centered>yes</centered>" +
                  "<zoom>no</zoom>" +
                  "<texture>#Play.Current.Thumb</texture>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!string.equals(#Play.Current.thumb,)+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>Music logo Animation</description>" +
                  "<type>animation</type>" +
                  "<id>7210</id>" +
                  "<posX>" + (baseXpos + 18).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 18).ToString() + "</posY>" +
                  "<width>114</width>" +
                  "<height>114</height>" +
                  "<textures>#Play.Current.Thumb;" + "#StreamedMP.ArtistPath\\#Play.Current.ArtistL.jpg</textures>" +
                  "<Duration>0:0:45</Duration>" +
                  "<randomize>no</randomize>" +
                  "<keepaspectratio>no</keepaspectratio>" +
                  "<centered>yes</centered>" +
                  "<zoom>yes</zoom>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+control.hasthumb(7220)+control.hasthumb(7230)+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>Selectable Button</description>" +
                  "<type>button</type>" +
                  "<id>17011</id>" +
                  "<posX>" + (baseXpos + 13).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 13).ToString() + "</posY>" +
                  "<width>124</width>" +
                  "<height>124</height>" +
                  "<textureFocus>thumbborder5.png</textureFocus>" +
                  "<textureNoFocus>-</textureNoFocus>" +
                  "<hyperlink>510</hyperlink>" +
                  "<!--<action>18</action>-->" +
                  "<onup>7777</onup>" +
                  "<ondown>7777</ondown>" +
                  "<onright>7777</onright>" +
                  "<onleft>7777</onleft>" +
                  "<colordiffuse>f1ffffff</colordiffuse>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>artist info label</description>" +
                  "<type>fadelabel</type>" +
                  "<id>0</id>" +
                  "<width>340</width>" +
                  "<height>40</height>" +
                  "<posX>" + (baseXpos + 143).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 23).ToString() + "</posY>" +
                  "<label>#Play.Current.Artist</label>" +
                  "<align>left</align>" +
                  "<textcolor>FFFFFFFF</textcolor>" +
                  "<font>mediastream12tc</font>" +
                  "<scrollStartDelaySec>30</scrollStartDelaySec>" +
                  "<wrapString> | </wrapString>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>title info label</description>" +
                  "<type>fadelabel</type>" +
                  "<id>0</id>" +
                  "<width>340</width>" +
                  "<height>40</height>" +
                  "<posX>" + (baseXpos + 143).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 44).ToString() + "</posY>" +
                  "<label>#Play.Current.Title</label>" +
                  "<align>left</align>" +
                  "<textcolor>FFFFFFFF</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>30</scrollStartDelaySec>" +
                  "<wrapString> | </wrapString>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>album info label</description>" +
                  "<type>fadelabel</type>" +
                  "<id>0</id>" +
                  "<width>340</width>" +
                  "<height>40</height>" +
                  "<posX>" + (baseXpos + 143).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 63).ToString() + "</posY>" +
                  "<label>#Play.Current.Album</label>" +
                  "<align>left</align>" +
                  "<textcolor>FFFFFFFF</textcolor>" +
                  "<font>mediastream10c</font>" +
                  "<scrollStartDelaySec>30</scrollStartDelaySec>" +
                  "<wrapString> | </wrapString>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>play time / duration label</description>" +
                  "<type>fadelabel</type>" +
                  "<id>0</id>" +
                  "<width>340</width>" +
                  "<height>40</height>" +
                  "<posX>" + (baseXpos + 143).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 81).ToString() + "</posY>" +
                  "<align>left</align>" +
                  "<label>#currentplaytime / #duration</label>" +
                  "<textcolor>FFFFFFFF</textcolor>" +
                  "<font>mediastream10c</font>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>next song label</description>" +
                  "<type>fadelabel</type>" +
                  "<id>0</id>" +
                  "<width>42</width>" +
                  "<height>40</height>" +
                  "<posX>" + (baseXpos + 143).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 107).ToString() + "</posY>" +
                  "<label>Next</label>" +
                  "<textcolor>FF025984</textcolor>" +
                  "<font>mediastream10</font>" +
                  "<align>left</align>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+control.isvisible(3338)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>next song info label</description>" +
                  "<type>fadelabel</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXpos + 180).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 107).ToString() + "</posY>" +
                  "<width>300</width>" +
                  "<height>40</height>" +
                  "<label>: #Play.Next.Title by #Play.Next.Artist</label>" +
                  "<textcolor>white</textcolor>" +
                  "<align>left</align>" +
                  "<font>mediastream10</font>" +
                  "<scrollStartDelaySec>30</scrollStartDelaySec>" +
                  "<wrapString> | </wrapString>" +
                  "<visible>player.hasmedia+control.isvisible(3337)+control.isvisible(3338)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<!--                                    :: VIDEO OVERLAY IN BASIC HOME ::                                    -->" +
                "<control>" +
                  "<description>video preview window BACKGROUND</description>" +
                  "<type>button</type>" +
                  "<id>17</id>" +
                  "<posX>" + (baseXpos + 18).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 17).ToString() + "</posY>" +
                  "<width>203</width>" +
                  "<height>116</height>" +
                  "<textureFocus>video_window_focus.png</textureFocus>" +
                  "<textureNoFocus>-</textureNoFocus>" +
                  "<action>18</action>" +
                  "<onup>7777</onup>" +
                  "<ondown>7777</ondown>" +
                  "<onright>7777</onright>" +
                  "<onleft>7777</onleft>" +
                  "<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>video preview window</description>" +
                  "<type>videowindow</type>" +
                  "<id>99</id>" +
                  "<posX>" + (baseXpos + 26).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 25).ToString() + "</posY>" +
                  "<width>180</width>" +
                  "<height>103</height>" +
                  "<textureFocus>tv_blue_border.png</textureFocus>" +
                  "<action>18</action>" +
                  "<onup>7777</onup>" +
                  "<ondown>7777</ondown>" +
                  "<onright>7777</onright>" +
                  "<onleft>7777</onleft>" +
                  "<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>artist info label</description>" +
                  "<type>fadelabel</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXpos + 233).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 20).ToString() + "</posY>" +
                  "<width>250</width>" +
                  "<height>40</height>" +
                  "<label>#Play.Current.Title</label>" +
                  "<textcolor>FFFFFFFF</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>30</scrollStartDelaySec>" +
                  "<wrapString> | </wrapString>" +
                  "<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>title info label</description>" +
                  "<type>fadelabel</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXpos + 233).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 38).ToString() + "</posY>" +
                  "<width>250</width>" +
                  "<height>40</height>" +
                  "<label>#Play.Current.Year</label>" +
                  "<textcolor>FFFFFFFF</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>30</scrollStartDelaySec>" +
                  "<wrapString> | </wrapString>" +
                  "<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>album info label</description>" +
                  "<type>fadelabel</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXpos + 233).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 64).ToString() + "</posY>" +
                  "<width>250</width>" +
                  "<height>40</height>" +
                  "<label>#Play.Current.Genre</label>" +
                  "<textcolor>FFFFFFFF</textcolor>" +
                  "<font>mediastream10c</font>" +
                  "<scrollStartDelaySec>30</scrollStartDelaySec>" +
                  "<wrapString> | </wrapString>" +
                  "<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>album info label</description>" +
                  "<type>fadelabel</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXpos + 233).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 84).ToString() + "</posY>" +
                  "<width>250</width>" +
                  "<height>40</height>" +
                  "<label>#Play.Current.Director</label>" +
                  "<textcolor>FFFFFFFF</textcolor>" +
                  "<font>mediastream10c</font>" +
                  "<scrollStartDelaySec>30</scrollStartDelaySec>" +
                  "<wrapString> | </wrapString>" +
                  "<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
                "<control>" +
                  "<description>play time / duration label</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXpos + 233).ToString() + "</posX>" +
                  "<posY>" + (overlayYpos + 104).ToString() + "</posY>" +
                  "<width>250</width>" +
                  "<height>40</height>" +
                  "<label>#currentplaytime / #duration</label>" +
                  "<textcolor>FFFFFFFF</textcolor>" +
                  "<font>mediastream10c</font>" +
                  "<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)" + hideControls + " + string.equals(#StreamedMP.VideoWallpaper,false)</visible>" +
                "</control>" +
              "</controls>" +
            "</window>";

      writeXMLFile("common.overlay.basichome.xml");
    }
  }
}
