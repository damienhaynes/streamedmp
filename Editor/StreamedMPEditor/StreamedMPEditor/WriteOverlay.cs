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
    private void generateOverlay(int baseYpos, int weatherId)
    {
      int overlayYpos = 0;
      int endYpos = 0;
      int maxEndYpos = 540;
      int overlayOffset = 140;
      string overlayFanart = "No";
      string hideControls = "!control.isvisible(" + int.Parse(weatherId.ToString()) + ")";

      if (cbMostRecentTvSeries.Checked)
        hideControls += "+!control.isvisible(" + basicHomeValues.tvseriesControl.ToString() + ")";

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

      if (cbOverlayFanart.Checked)
        overlayFanart = "Yes";

      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + " standalone=" + quote + "yes" + quote + "?>\n" +
            "<!--\n\n" +
            "different overlays on home screens\n\n" +
            "-->\n\n" +
            "<window>\n\n" +
              "\t<controls>\n" +
              "\t<!--                                    :: DUMMY / BACKGROUND ::                                    -->\n" +
                "\t\t<control>\n" +
                "\t\t\t<description>dummy (To enable fanart set this control to visible)</description>\n" +
                  "\t\t\t<type>label</type>\n" +
                  "\t\t\t<id>3339</id>\n" +
                  "\t\t\t<posX>2000</posX>\n" +
                  "\t\t\t<label></label>\n" +
                  "\t\t\t<visible>" + overlayFanart + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>DUMMY CONTROL FOR FANART 1 RANDOM VISIBILITY CONDITION</description>\n" +
                  "\t\t\t<type>label</type>\n" +
                  "\t\t\t<id>91919297</id>\n" +
                  "\t\t\t<posX>0</posX>\n" +
                  "\t\t\t<posY>0</posY>\n" +
                  "\t\t\t<width>1</width>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>DUMMY CONTROL FOR FANART 2 RANDOM VISIBILITY CONDITION</description>\n" +
                  "\t\t\t<type>label</type>\n" +
                  "\t\t\t<id>91919298</id>\n" +
                  "\t\t\t<posX>0</posX>\n" +
                  "\t\t\t<posY>0</posY>\n" +
                  "\t\t\t<width>1</width>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>DUMMY CONTROL FOR FANART 1 PLAY VISIBILITY CONDITION</description>\n" +
                  "\t\t\t<type>label</type>\n" +
                  "\t\t\t<id>91919295</id>\n" +
                  "\t\t\t<posX>0</posX>\n" +
                  "\t\t\t<posY>0</posY>\n" +
                  "\t\t\t<width>1</width>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>DUMMY CONTROL FOR FANART 2 PLAY VISIBILITY CONDITION</description>\n" +
                  "\t\t\t<type>label</type>\n" +
                  "\t\t\t<id>91919296</id>\n" +
                  "\t\t\t<posX>0</posX>\n" +
                  "\t\t\t<posY>0</posY>\n" +
                  "\t\t\t<width>1</width>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                "\t\t\t<description>DUMMY CONTROL FOR FANART AVAILABILITY CONDITION</description>\n" +
                  "\t\t\t<type>label</type>\n" +
                  "\t\t\t<id>91919294</id>\n" +
                  "\t\t\t<posX>0</posX>\n" +
                  "\t\t\t<posY>0</posY>\n" +
                  "\t\t\t<width>1</width>\n" +
                  "\t\t\t<visible>no</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>dummy (visible when music is playing)</description>\n" +
                  "\t\t\t<type>label</type>\n" +
                  "\t\t\t<id>3337</id>\n" +
                  "\t\t\t<posX>2000</posX>\n" +
                  "\t\t\t<label>#Play.Current.Album</label>\n" +
                  "\t\t\t<visible>!player.hasvideo+player.hasaudio</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>dummy (visible when there is a next track)</description>\n" +
                  "\t\t\t<type>label</type>\n" +
                  "\t\t\t<id>3338</id>\n" +
                  "\t\t\t<posX>2000</posX>\n" +
                  "\t\t\t<label>#Play.Next.Title</label>\n" +
                  "\t\t\t<visible>player.hasmedia+control.hastext(3338)</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>dialog bg</description>\n" +
                  "\t\t\t<type>image</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<posX>765</posX>\n" +
                  "\t\t\t<posY>" + overlayYpos.ToString() + "</posY>\n" +
                  "\t\t\t<width>519</width>\n" +
                  "\t\t\t<height>152</height>\n" +
                  "\t\t\t<texture>dialogprogressbg.png</texture>\n" +
                  "\t\t\t<visible>player.hasmedia+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                  "\t\t\t<!-- Not visible in regular home and my plugins -->\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>PICTURES NOW PLAYING BACKGROUND 1</description>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<type>image</type>\n" +
                  "\t\t\t<posX>990</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 18).ToString() + "</posY>\n" +
                  "\t\t\t<width>270</width>\n" +
                  "\t\t\t<height>114</height>\n" +
                  "\t\t\t<texture>#fanarthandler.music.backdrop1.play</texture>\n" +
                  "\t\t\t<visible>control.isvisible(3339)+Player.HasMedia+control.isvisible(91919295)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                  "\t\t\t<animation effect=" + quote + "fade" + quote + " start=" + quote + "10" + quote + " end=" + quote + "100" + quote + " time=" + quote + "1000" + quote + ">VisibleChange</animation>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>PICTURES NOW PLAYING BACKGROUND 2</description>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<type>image</type>\n" +
                  "\t\t\t<posX>990</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 18).ToString() + "</posY>\n" +
                  "\t\t\t<width>270</width>\n" +
                  "\t\t\t<height>114</height>\n" +
                  "\t\t\t<texture>#fanarthandler.music.backdrop2.play</texture>\n" +
                  "\t\t\t<visible>control.isvisible(3339)+Player.HasMedia+control.isvisible(91919296)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767+" + hideControls + "</visible>\n" +
                  "\t\t\t<animation effect=" + quote + "fade" + quote + " start=" + quote + "10" + quote + " end=" + quote + "100" + quote + " time=" + quote + "1000" + quote + ">VisibleChange</animation>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>Overlay Mask</description>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<type>image</type>\n" +
                  "\t\t\t<posX>783</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 18).ToString() + "</posY>\n" +
                  "\t\t\t<width>512</width>\n" +
                  "\t\t\t<height>114</height>\n" +
                  "\t\t\t<colordiffuse>EFFFFFFF</colordiffuse>\n" +
                  "\t\t\t<texture>overlayfanartmask.png</texture>\n" +
                  "\t\t\t<visible>control.isvisible(3339)+Player.HasMedia+control.isvisible(91919294)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t\t<!--                                    :: MUSIC OVERLAY IN BASIC HOME ::                                    -->\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>music logo</description>\n" +
                  "\t\t\t<type>image</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<posX>783</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 18).ToString() + "</posY>\n" +
                  "\t\t\t<width>114</width>\n" +
                  "\t\t\t<height>114</height>\n" +
                  "\t\t\t<keepaspectratio>yes</keepaspectratio>\n" +
                  "\t\t\t<centered>yes</centered>\n" +
                  "\t\t\t<zoom>no</zoom>\n" +
                  "\t\t\t<texture>defaultAudioBig.png</texture>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+string.equals(#Play.Current.Thumb,)+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>music logo</description>\n" +
                  "\t\t\t<type>image</type>\n" +
                  "\t\t\t<id>7220</id>\n" +
                  "\t\t\t<posX>783</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 18).ToString() + "</posY>\n" +
                  "\t\t\t<width>114</width>\n" +
                  "\t\t\t<height>114</height>\n" +
                  "\t\t\t<keepaspectratio>no</keepaspectratio>\n" +
                  "\t\t\t<centered>yes</centered>\n" +
                  "\t\t\t<zoom>yes</zoom>\n" +
                  "\t\t\t<texture>" + mpPaths.thumbsPath + "Music\\Artists\\#Play.Current.ArtistL.jpg</texture>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+string.equals(#Play.Current.Thumb,)+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>music logo</description>\n" +
                  "\t\t\t<type>image</type>\n" +
                  "\t\t\t<id>7230</id>\n" +
                  "\t\t\t<posX>783</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 18).ToString() + "</posY>\n" +
                  "\t\t\t<width>114</width>\n" +
                  "\t\t\t<height>114</height>\n" +
                  "\t\t\t<keepaspectratio>yes</keepaspectratio>\n" +
                  "\t\t\t<centered>yes</centered>\n" +
                  "\t\t\t<zoom>no</zoom>\n" +
                  "\t\t\t<texture>#Play.Current.Thumb</texture>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!string.equals(#Play.Current.Thumb,)+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>Music logo Animation</description>\n" +
                  "\t\t\t<type>animation</type>\n" +
                  "\t\t\t<id>7210</id>\n" +
                  "\t\t\t<posX>783</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 18).ToString() + "</posY>\n" +
                  "\t\t\t<width>114</width>\n" +
                  "\t\t\t<height>114</height>\n" +
                  "\t\t\t<textures>#Play.Current.Thumb;" + mpPaths.thumbsPath + "Music\\Artists\\#Play.Current.ArtistL.jpg</textures>\n" +
                  "\t\t\t<Duration>0:0:45</Duration>\n" +
                  "\t\t\t<randomize>no</randomize>\n" +
                  "\t\t\t<keepaspectratio>no</keepaspectratio>\n" +
                  "\t\t\t<centered>yes</centered>\n" +
                  "\t\t\t<zoom>yes</zoom>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+control.hasthumb(7220)+control.hasthumb(7230)+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>Selectable Button</description>\n" +
                  "\t\t\t<type>button</type>\n" +
                  "\t\t\t<id>17011</id>\n" +
                  "\t\t\t<posX>778</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 13).ToString() + "</posY>\n" +
                  "\t\t\t<width>124</width>\n" +
                  "\t\t\t<height>124</height>\n" +
                  "\t\t\t<textureFocus>thumbborder5.png</textureFocus>\n" +
                  "\t\t\t<textureNoFocus>-</textureNoFocus>\n" +
                  "\t\t\t<hyperlink>510</hyperlink>\n" +
                  "\t\t\t<!--<action>18</action>-->\n" +
                  "\t\t\t<onup>7777</onup>\n" +
                  "\t\t\t<ondown>7777</ondown>\n" +
                  "\t\t\t<onright>7777</onright>\n" +
                  "\t\t\t<onleft>7777</onleft>\n" +
                  "\t\t\t<colordiffuse>f1ffffff</colordiffuse>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>artist info label</description>\n" +
                  "\t\t\t<type>fadelabel</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<width>340</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<posX>908</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 23).ToString() + "</posY>\n" +
                  "\t\t\t<label>#Play.Current.Artist</label>\n" +
                  "\t\t\t<align>left</align>\n" +
                  "\t\t\t<textcolor>FFFFFFFF</textcolor>\n" +
                  "\t\t\t<font>mediastream12tc</font>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>title info label</description>\n" +
                  "\t\t\t<type>fadelabel</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<width>340</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<posX>908</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 44).ToString() + "</posY>\n" +
                  "\t\t\t<label>#Play.Current.Title</label>\n" +
                  "\t\t\t<align>left</align>\n" +
                  "\t\t\t<textcolor>FFFFFFFF</textcolor>\n" +
                  "\t\t\t<font>mediastream10tc</font>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>album info label</description>\n" +
                  "\t\t\t<type>fadelabel</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<width>340</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<posX>908</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 63).ToString() + "</posY>\n" +
                  "\t\t\t<label>#Play.Current.Album</label>\n" +
                  "\t\t\t<align>left</align>\n" +
                  "\t\t\t<textcolor>FFFFFFFF</textcolor>\n" +
                  "\t\t\t<font>mediastream10c</font>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>play time / duration label</description>\n" +
                  "\t\t\t<type>fadelabel</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<width>340</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<posX>908</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 81).ToString() + "</posY>\n" +
                  "\t\t\t<align>left</align>\n" +
                  "\t\t\t<label>#currentplaytime / #duration</label>\n" +
                  "\t\t\t<textcolor>FFFFFFFF</textcolor>\n" +
                  "\t\t\t<font>mediastream10c</font>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>next song label</description>\n" +
                  "\t\t\t<type>fadelabel</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<width>42</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<posX>908</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 107).ToString() + "</posY>\n" +
                  "\t\t\t<label>Next</label>\n" +
                  "\t\t\t<textcolor>FF025984</textcolor>\n" +
                  "\t\t\t<font>mediastream10</font>\n" +
                  "\t\t\t<align>left</align>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+control.isvisible(3338)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>next song info label</description>\n" +
                  "\t\t\t<type>fadelabel</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<posX>945</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 107).ToString() + "</posY>\n" +
                  "\t\t\t<width>300</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<label>: #Play.Next.Title by #Play.Next.Artist</label>\n" +
                  "\t\t\t<textcolor>white</textcolor>\n" +
                  "\t\t\t<align>left</align>\n" +
                  "\t\t\t<font>mediastream10</font>\n" +
                  "\t\t\t<visible>player.hasmedia+control.isvisible(3337)+control.isvisible(3338)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t\t<!--                                    :: VIDEO OVERLAY IN BASIC HOME ::                                    -->\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>video preview window BACKGROUND</description>\n" +
                  "\t\t\t<type>button</type>\n" +
                  "\t\t\t<id>17</id>\n" +
                  "\t\t\t<posX>783</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 17).ToString() + "</posY>\n" +
                  "\t\t\t<width>203</width>\n" +
                  "\t\t\t<height>116</height>\n" +
                  "\t\t\t<textureFocus>video_window_focus.png</textureFocus>\n" +
                  "\t\t\t<textureNoFocus>-</textureNoFocus>\n" +
                  "\t\t\t<action>18</action>\n" +
                  "\t\t\t<onup>7777</onup>\n" +
                  "\t\t\t<ondown>7777</ondown>\n" +
                  "\t\t\t<onright>7777</onright>\n" +
                  "\t\t\t<onleft>7777</onleft>\n" +
                  "\t\t\t<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>video preview window</description>\n" +
                  "\t\t\t<type>videowindow</type>\n" +
                  "\t\t\t<id>99</id>\n" +
                  "\t\t\t<posX>791</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 25).ToString() + "</posY>\n" +
                  "\t\t\t<width>180</width>\n" +
                  "\t\t\t<height>103</height>\n" +
                  "\t\t\t<textureFocus>tv_blue_border.png</textureFocus>\n" +
                  "\t\t\t<action>18</action>\n" +
                  "\t\t\t<onup>7777</onup>\n" +
                  "\t\t\t<ondown>7777</ondown>\n" +
                  "\t\t\t<onright>7777</onright>\n" +
                  "\t\t\t<onleft>7777</onleft>\n" +
                  "\t\t\t<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>artist info label</description>\n" +
                  "\t\t\t<type>fadelabel</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<posX>998</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 20).ToString() + "</posY>\n" +
                  "\t\t\t<width>250</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<label>#Play.Current.Title</label>\n" +
                  "\t\t\t<textcolor>FFFFFFFF</textcolor>\n" +
                  "\t\t\t<font>mediastream10tc</font>\n" +
                  "\t\t\t<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>title info label</description>\n" +
                  "\t\t\t<type>fadelabel</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<posX>998</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 38).ToString() + "</posY>\n" +
                  "\t\t\t<width>250</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<label>#Play.Current.Year</label>\n" +
                  "\t\t\t<textcolor>FFFFFFFF</textcolor>\n" +
                  "\t\t\t<font>mediastream10tc</font>\n" +
                  "\t\t\t<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>album info label</description>\n" +
                  "\t\t\t<type>fadelabel</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<posX>998</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 64).ToString() + "</posY>\n" +
                  "\t\t\t<width>250</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<label>#Play.Current.Genre</label>\n" +
                  "\t\t\t<textcolor>FFFFFFFF</textcolor>\n" +
                  "\t\t\t<font>mediastream10c</font>\n" +
                  "\t\t\t<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>album info label</description>\n" +
                  "\t\t\t<type>fadelabel</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<posX>998</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 84).ToString() + "</posY>\n" +
                  "\t\t\t<width>250</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<label>#Play.Current.Director</label>\n" +
                  "\t\t\t<textcolor>FFFFFFFF</textcolor>\n" +
                  "\t\t\t<font>mediastream10c</font>\n" +
                  "\t\t\t<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
                "\t\t<control>\n" +
                  "\t\t\t<description>play time / duration label</description>\n" +
                  "\t\t\t<type>label</type>\n" +
                  "\t\t\t<id>0</id>\n" +
                  "\t\t\t<posX>998</posX>\n" +
                  "\t\t\t<posY>" + (overlayYpos + 104).ToString() + "</posY>\n" +
                  "\t\t\t<width>250</width>\n" +
                  "\t\t\t<height>40</height>\n" +
                  "\t\t\t<label>#currentplaytime / #duration</label>\n" +
                  "\t\t\t<textcolor>FFFFFFFF</textcolor>\n" +
                  "\t\t\t<font>mediastream10c</font>\n" +
                  "\t\t\t<visible>player.hasmedia+!control.isvisible(3337)+![window.istopmost(0)|window.istopmost(34)]+!control.isvisible(6767)+" + hideControls + "</visible>\n" +
                "\t\t</control>\n" +
              "\t</controls>\n" +
            "</window>";


      StreamWriter writer;
      if (System.IO.File.Exists(mpPaths.streamedMPpath + "common.overlay.basichome.xml"))
        System.IO.File.Delete(mpPaths.streamedMPpath + "common.overlay.basichome.xml");
      writer = System.IO.File.CreateText(mpPaths.streamedMPpath + "common.overlay.basichome.xml");
      writer.Write(xml);
      writer.Close();
    }
  }
}
