namespace StreamedMPEditor
{
  public partial class streamedMpEditor
  {

    #region Main

    void generateMostRecentOverlay(chosenMenuStyle menuStyle, isOverlayType isOverlay)
    {
      if (isOverlay == isOverlayType.TVSeries)
      {
        doTVSeries(tvSeriesRecentStyle);
      }
      if (isOverlay == isOverlayType.MovPics)
      {
        doMovingPictures(movPicsRecentStyle);
      }
    }
    //
    // TVSeries - Summary or Full, vertical or horizonal
    //
    void doTVSeries(tvSeriesRecentType reqType)
    {
      if (menuStyle == chosenMenuStyle.verticalStyle)
      {
        if (reqType == tvSeriesRecentType.summary)
        {
          verticalTVSeriesSummary();
        }
        else
        {
          verticalTVSeriesFull();
        }
      }
      else
      {
        if (reqType == tvSeriesRecentType.summary)
        {
          horizontalTVSeriesSummary();
        }
        else
        {
          horizonalTVSeriesFull();
        }
      }
    }
    //
    // MovingPictures - Summary or Full, vertical or horizonal
    //
    void doMovingPictures(movPicsRecentType reqType)
    {
      if (menuStyle == chosenMenuStyle.verticalStyle)
      {
        if (reqType == movPicsRecentType.summary)
        {
          verticalMovingPicturesSummary();
        }
        else
        {
          verticalMovingPicturesFull();
        }
      }
      else
      {
        if (reqType == movPicsRecentType.summary)
        {
          horizontalMovingPicturesSummary();
        }
        else
        {
         horizonalMovingPicturesFull();
        }
      }
    }


    #endregion

    #region TVSeries Vertical Full

    void verticalTVSeriesFull()
    {
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
            "<window>\n" +
              "<controls>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#infoservice.recentlyAdded.series1.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 1 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>70</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>900</posX>\n" +
                    "<posY>79</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#infoservice.recentlyAdded.series1.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>71</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>98</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#infoservice.recentlyAdded.series1.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>151</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series1.episodetitle</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>206</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series1.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series1.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#infoservice.recentlyAdded.series2.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 2 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>270</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>902</posX>\n" +
                    "<posY>279</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#infoservice.recentlyAdded.series2.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>270</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>299</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#infoservice.recentlyAdded.series2.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>352</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series2.episodetitle</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>405</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series2.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series2.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#infoservice.recentlyAdded.series3.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 3 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>470</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>901</posX>\n" +
                    "<posY>478</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#infoservice.recentlyAdded.series3.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>470</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 name</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>498</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series3.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 title</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>551</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series3.episodetitle</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>604</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series3.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series3.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
              "</controls>\n" +
            "</window>";

      writeXMLFile("basichome.recentlyadded.tvseries.VFull.xml");

    }

    #endregion

    #region TVSeries Vertical Summary

    void verticalTVSeriesSummary()
    {
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.poster)
      {
        buildTVSeriesSummaryFile(475, mostRecentTVSeriesSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.tvseries.VSum.xml");
      }
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.fanart)
      {
        buildTVSeriesSummaryFile(475, mostRecentTVSeriesSummaryStyle.fanart);
        writeXMLFile("basichome.recentlyadded.tvseries.VSum2.xml");
      }
    }

    #endregion

    #region TVSeries Horizontal Full

    void horizonalTVSeriesFull()
    {
      //
      // Work out the Ypos
      //
      int overlayYpos = 0;
      int maxYpos = 536;
      int overlayOffset = 184;
      int hdOffset = 0;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + 54;
      int menuBot = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu;

      if (screenres == screenResolutionType.res1920x1080)
        hdOffset = 0;
      else
        hdOffset = 0;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 30;

      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
        menuBot += (basicHomeValues.subMenuHeight - 30);

      if (menuBot > maxYpos)
      {
        overlayYpos = menuTop - overlayOffset;
        if (screenres == screenResolutionType.res1920x1080)
          hdOffset -= 54;
      }
      else
        overlayYpos = 536;

        // Build the file
        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#infoservice.recentlyAdded.series1.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 1 BG</description>" +
                      "<posX>28</posX>" +
                      "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                      "<type>image</type>" +
                      "<id>6777</id>" +
                      "<width>385</width>" +
                      "<height>180</height>" +
                      "<texture>recentfullbg.png</texture>" +
                      "<colordiffuse>EEFFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 1 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>36</posX>" +
                      "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                      "<height>163</height>" +
                      "<width>111</width>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#infoservice.recentlyAdded.series1.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 1 Rounded Cover</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>26</posX>" +
                      "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                      "<height>181</height>" +
                      "<width>129</width>" +
                      "<texture>round.poster.frame.noreflection.png</texture>" +
                    "</control>      " +
                  "<control>" +
                      "<description>Series 1 name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>152</posX>" +
                      "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                      "<width>258</width>" +
                      "<label>#infoservice.recentlyAdded.series1.title</label>" +
                      "<font>mediastream12tc</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 1 title</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>152</posX>" +
                      "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                      "<width>255</width>" +
                      "<label>#infoservice.recentlyAdded.series1.episodetitle</label>" +
                      "<font>mediastream12</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 1 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>152</posX>" +
                      "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                      "<width>255</width>" +
                      "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series1.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series1.episodenumber</label>" +
                      "<font>mediastream10</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#infoservice.recentlyAdded.series2.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 2 BG</description>" +
                      "<posX>442</posX>" +
                      "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                      "<type>image</type>" +
                      "<id>6777</id>" +
                      "<width>385</width>" +
                      "<height>180</height>" +
                      "<texture>recentfullbg.png</texture>" +
                      "<colordiffuse>EEFFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>452</posX>" +
                      "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                      "<height>163</height>" +
                      "<width>111</width>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#infoservice.recentlyAdded.series2.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 Rounded Cover</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>440</posX>" +
                      "<posY>" + (overlayYpos + 1).ToString() + "</posY>" +
                      "<height>181</height>" +
                      "<width>127</width>" +
                      "<texture>round.poster.frame.noreflection.png</texture>" +
                    "</control>      " +
                    "<control>" +
                      "<description>Series 2 name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>568</posX>" +
                      "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                      "<width>258</width>" +
                      "<label>#infoservice.recentlyAdded.series2.title</label>" +
                      "<font>mediastream12tc</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 title</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>565</posX>" +
                      "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                      "<width>255</width>" +
                      "<label>#infoservice.recentlyAdded.series2.episodetitle</label>" +
                      "<font>mediastream12</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>565</posX>" +
                      "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                      "<width>258</width>" +
                      "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series2.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series2.episodenumber</label>" +
                      "<font>mediastream10</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#infoservice.recentlyAdded.series3.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 3 BG</description>" +
                      "<posX>855</posX>" +
                      "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                      "<type>image</type>" +
                      "<id>6777</id>" +
                      "<width>385</width>" +
                      "<height>180</height>" +
                      "<texture>recentfullbg.png</texture>" +
                      "<colordiffuse>EEFFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>866</posX>" +
                      "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                      "<height>163</height>" +
                      "<width>111</width>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#infoservice.recentlyAdded.series3.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 Rounded Cover</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>858</posX>" +
                      "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                      "<height>181</height>" +
                      "<width>127</width>" +
                      "<texture>round.poster.frame.noreflection.png</texture>" +
                    "</control>      " +
                    "<control>" +
                      "<description>Series 3 name</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>981</posX>" +
                      "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                      "<width>255</width>" +
                      "<label>#infoservice.recentlyAdded.series3.title</label>" +
                      "<font>mediastream12tc</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 title</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>981</posX>" +
                      "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                      "<width>255</width>" +
                      "<label>#infoservice.recentlyAdded.series3.episodetitle</label>" +
                      "<font>mediastream12</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>981</posX>" +
                      "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                      "<width>255</width>" +
                      "<label>#TVSeries.Translation.Season.Label: #infoservice.recentlyAdded.series3.season #TVSeries.Translation.Episode.Label: #infoservice.recentlyAdded.series3.episodenumber</label>" +
                      "<font>mediastream10</font>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "</controls>" +
              "</window>";

      writeXMLFile("basichome.recentlyadded.tvseries.HFull.xml");

    }

    #endregion

    #region TVSeries Horizontal Summary

    void horizontalTVSeriesSummary()
    {
      //
      // Work out the Ypos
      //
      int overlayYpos = 0;
      int overlayOffset = 224;
      int menuOffset = 50;
      if (menuStyle == chosenMenuStyle.horizontalContextStyle || horizontalContextLabels.Checked)
        menuOffset = 60;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + menuOffset;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 30;

      overlayYpos = menuTop - overlayOffset;
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.poster)
      {
        buildTVSeriesSummaryFile(overlayYpos, mostRecentTVSeriesSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.tvseries.HSum.xml");
      }
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.fanart)
      {
        buildTVSeriesSummaryFile(overlayYpos, mostRecentTVSeriesSummaryStyle.fanart);
        writeXMLFile("basichome.recentlyadded.tvseries.HSum2.xml");
      }


    }

    #endregion

    #region TVSeries Summary Builder

    void buildTVSeriesSummaryFile(int overlayYpos, mostRecentTVSeriesSummaryStyle sumStyle)
    {
      #region Summary Style 1

      if (sumStyle == mostRecentTVSeriesSummaryStyle.poster)
      {
        // Build the file
        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 3</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#infoservice.recentlyAdded.series3.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>861</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>871</posX>" +
                      "<posY>" + (overlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#infoservice.recentlyAdded.series3.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>871</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#infoservice.recentlyAdded.series3.seasonx#infoservice.recentlyAdded.series3.episodenumber</label>" +
                      "<font>mediastream10tc</font>" +
                      "<align>center</align>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 2</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#infoservice.recentlyAdded.series2.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1001</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1011</posX>" +
                      "<posY>" + (overlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#infoservice.recentlyAdded.series2.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1011</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#infoservice.recentlyAdded.series2.seasonx#infoservice.recentlyAdded.series2.episodenumber</label>" +
                      "<font>mediastream10tc</font>" +
                      "<align>center</align>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 1</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#infoservice.recentlyAdded.series1.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1141</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1151</posX>" +
                      "<posY>" + (overlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#infoservice.recentlyAdded.series1.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1151</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#infoservice.recentlyAdded.series1.seasonx#infoservice.recentlyAdded.series1.episodenumber</label>" +
                      "<font>mediastream10tc</font>" +
                      "<align>center</align>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
      }

#endregion

      #region Summary Style 2

      if (sumStyle == mostRecentTVSeriesSummaryStyle.fanart)
      {

        string mrSeriesNameFont = tvSeriesOptions.mrSeriesFont;
        string mrEpisodeFont = tvSeriesOptions.mrEpisodeFont;
        bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;

        

        string fanartProperty = "#infoservice.recentlyAdded.series1.fanart";

        if (cbCycleFanart.Checked)
          fanartProperty = "#StreamedMP.MostRecentImageFanart";

        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
              "<window>\n" +
                "<controls>\n" +
                  "<control>\n" +
                    "<description>GROUP: RecentlyAdded Series</description>\n" +
                    "<type>group</type>\n" +
                    "<dimColor>0xffffffff</dimColor>\n" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+![string.starts(#infoservice.recentlyAdded.series1.fanart,#)|string.starts(#infoservice.recentlyAdded.series1.thumb,#)]</visible>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                    "<control>\n" +
                    "<description>Series 1 BG</description>\n" +
                    "<posX>976</posX>\n" +
                    "<posY>50</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>306</width>\n" +
                    "<height>320</height>\n" +
                    "<texture>recentsummoverlaybg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Header label</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>70</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.LatestEpisodes</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>      " +
                  "<control>\n" +
                    "<description>Series 1 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>245</posY>\n" +
                    "<width>258</width>\n";
                    if (mrSeriesTitleLast)
                      xml += "<label>#StreamedMP.MostRecent.1.SEFormat - #infoservice.recentlyAdded.series1.title</label>\n";
                    else
                      xml += "<label>#infoservice.recentlyAdded.series1.title - #StreamedMP.MostRecent.1.SEFormat</label>\n";
                    xml += "<font>" + mrSeriesNameFont + "</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>262</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series1.episodetitle</label>\n" +
                    "<font>" + mrEpisodeFont + "</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>" +
                  "<control>\n" +
                    "<description>Series 1 thumb/fanart</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>92</posY>\n" +
                    "<width>268</width>\n" +
                    "<height>151</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>" + fanartProperty + "</texture>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+![string.starts(#infoservice.recentlyAdded.series2.fanart,#)|string.starts(#infoservice.recentlyAdded.series2.thumb,#)]</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                  "<control>\n" +
                    "<description>Series 2 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>280</posY>\n" +
                    "<width>258</width>\n";

                    if (mrSeriesTitleLast)
                      xml += "<label>#StreamedMP.MostRecent.2.SEFormat - #infoservice.recentlyAdded.series2.title</label>\n";
                    else
                      xml += "<label>#infoservice.recentlyAdded.series2.title - #StreamedMP.MostRecent.2.SEFormat</label>\n";
                    xml += "<font>" + mrSeriesNameFont + "</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>297</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series2.episodetitle</label>\n" +
                    "<font>" + mrEpisodeFont + "</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+![string.starts(#infoservice.recentlyAdded.series3.fanart,#)|string.starts(#infoservice.recentlyAdded.series3.thumb,#)]</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                  "<control>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>315</posY>\n" +
                    "<width>258</width>\n";
                    if (mrSeriesTitleLast)
                      xml += "<label>#StreamedMP.MostRecent.3.SEFormat - #infoservice.recentlyAdded.series3.title</label>\n";
                    else
                      xml += "<label>#infoservice.recentlyAdded.series3.title - #StreamedMP.MostRecent.3.SEFormat</label>\n";
                    xml += "<font>" + mrSeriesNameFont + "</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>332</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#infoservice.recentlyAdded.series3.episodetitle</label>\n" +
                    "<font>" + mrEpisodeFont + "</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
              "</controls>\n" +
            "</window>";
      }

      #endregion
    }
    #endregion

    #region MovingPictures Vertical Full

    void verticalMovingPicturesFull()
    {
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
            "<window>\n" +
              "<controls>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 1 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>70</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>900</posX>\n" +
                    "<posY>79</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.movie1.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>71</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>98</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>151</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.runtime</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>206</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.certification</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie2.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 2 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>270</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>902</posX>\n" +
                    "<posY>279</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.movie2.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>270</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>299</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>352</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>405</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.certification</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie3.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 3 BG</description>\n" +
                    "<posX>890</posX>\n" +
                    "<posY>470</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>385</width>\n" +
                    "<height>180</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>901</posX>\n" +
                    "<posY>478</posY>\n" +
                    "<width>112</width>\n" +
                    "<height>165</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.movie3.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>892</posX>\n" +
                    "<posY>470</posY>\n" +
                    "<height>181</height>\n" +
                    "<width>129</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 name</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>498</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie3.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 title</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>551</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie3.runtime</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>604</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie3.certification</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
              "</controls>\n" +
            "</window>";

      writeXMLFile("basichome.recentlyadded.movpics.VFull.xml");

    }

    #endregion

    #region MovingPictures Vertical Summary

    void verticalMovingPicturesSummary()
    {
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.poster)
      {
        buildMovingPicturesSummaryFile(475, mostRecentMovPicsSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.movpics.VSum.xml");
      }
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.fanart)
      {
        buildMovingPicturesSummaryFile(475, mostRecentMovPicsSummaryStyle.fanart);
        writeXMLFile("basichome.recentlyadded.movpics.VSum2.xml");
      }
    }

    #endregion

    #region MovingPictures Horizontal Full

    void horizonalMovingPicturesFull()
    {
      //
      // Work out the Ypos
      //
      int overlayYpos = 0;
      int maxYpos = 536;
      int overlayOffset = 184;
      int hdOffset = 0;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + 54;
      int menuBot = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu;

      if (screenres == screenResolutionType.res1920x1080)
        hdOffset = 0;
      else
        hdOffset = 0;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 30;

      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
        menuBot += (basicHomeValues.subMenuHeight - 30);

      if (menuBot > maxYpos)
      {
        overlayYpos = menuTop - overlayOffset;
        if (screenres == screenResolutionType.res1920x1080)
          hdOffset -= 54;
      }
      else
        overlayYpos = 536;

      // Build the file
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Movie 1</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Mivie 1 BG</description>" +
                    "<posX>28</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>385</width>" +
                    "<height>180</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>36</posX>" +
                    "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                    "<height>163</height>" +
                    "<width>111</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.movie1.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>26</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<height>181</height>" +
                    "<width>129</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                "<control>" +
                    "<description>Movie 2 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>152</posX>" +
                    "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                    "<width>258</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie1.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Runtime</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>152</posX>" +
                    "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie1.runtime</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Certification</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>152</posX>" +
                    "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie1.certification</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Movie 2</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie2.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Movie 2 BG</description>" +
                    "<posX>442</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>385</width>" +
                    "<height>180</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 2 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>452</posX>" +
                    "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                    "<height>163</height>" +
                    "<width>111</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.movie2.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>440</posX>" +
                    "<posY>" + (overlayYpos + 1).ToString() + "</posY>" +
                    "<height>181</height>" +
                    "<width>127</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                  "<control>" +
                    "<description>Movie 2 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>568</posX>" +
                    "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                    "<width>258</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie2.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 2 Runtime</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>565</posX>" +
                    "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 2 Certification</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>565</posX>" +
                    "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                    "<width>258</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie2.certification</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Movie 3</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie3.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Movie 3 BG</description>" +
                    "<posX>855</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>385</width>" +
                    "<height>180</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>866</posX>" +
                    "<posY>" + (overlayYpos + 10).ToString() + "</posY>" +
                    "<height>163</height>" +
                    "<width>111</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.movie3.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>858</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<height>181</height>" +
                    "<width>127</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                  "<control>" +
                    "<description>Movie 3 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>981</posX>" +
                    "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie3.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Runtime</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>981</posX>" +
                    "<posY>" + (overlayYpos + 82).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Certification</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>981</posX>" +
                    "<posY>" + (overlayYpos + 139).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie3.certification</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "</controls>" +
            "</window>";

      writeXMLFile("basichome.recentlyadded.movpics.HFull.xml");

    }

    #endregion

    #region MovingPictures Horizontal Summary

    void horizontalMovingPicturesSummary()
    {
      //
      // Work out the Ypos
      //
      int overlayYpos = 0;
      int overlayOffset = 224;
      int menuOffset = 50;
      if (menuStyle == chosenMenuStyle.horizontalContextStyle || horizontalContextLabels.Checked)
        menuOffset = 60;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + menuOffset;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 30;

      overlayYpos = menuTop - overlayOffset;
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.poster)
      {
        buildMovingPicturesSummaryFile(overlayYpos, mostRecentMovPicsSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.movpics.HSum.xml");
      }
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.fanart)
      {
        buildMovingPicturesSummaryFile(overlayYpos, mostRecentMovPicsSummaryStyle.fanart);
        writeXMLFile("basichome.recentlyadded.movpics.HSum2.xml");
      }


    }

    #endregion

    #region MovingPictures Summary Builder

    void buildMovingPicturesSummaryFile(int overlayYpos, mostRecentMovPicsSummaryStyle sumStyle)
    {
      #region Summary Style 1

      if (sumStyle == mostRecentMovPicsSummaryStyle.poster)
      {
        // Build the file
        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Movie 3</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie3.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>861</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>871</posX>" +
                      "<posY>" + (overlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.movie3.thumb</texture>" +
                    "</control>" +
                  "<control>" +
                    "<description>Movie 1 Star Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>871</posX>" +
                    "<posY>" + (overlayYpos + 185).ToString() + "</posY>" +
                    "<width>115</width>" +
                    "<height>22</height>" +
                    "<texture>star#StreamedMP.recentlyAdded.movie3.score.png</texture>" +
                    "<align>left</align>" +
                  "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 2</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie2.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1001</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1011</posX>" +
                      "<posY>" + (overlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.movie2.thumb</texture>" +
                    "</control>" +
                  "<control>" +
                    "<description>Movie 2 Star Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1011</posX>" +
                    "<posY>" + (overlayYpos + 185).ToString() + "</posY>" +
                    "<width>115</width>" +
                    "<height>22</height>" +
                    "<texture>star#StreamedMP.recentlyAdded.movie2.score.png</texture>" +
                    "<align>left</align>" +
                  "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 1</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 1500" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,200" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Movie 1 background</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1141</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>135</width>" +
                      "<height>220</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Movie 1 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1151</posX>" +
                      "<posY>" + (overlayYpos + 11).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>170</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.movie1.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Movie 1 Star rating</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1151</posX>" +
                      "<posY>" + (overlayYpos + 185).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<height>22</height>" +
                      "<texture>star#StreamedMP.recentlyAdded.movie1.score.png</texture>" +
                      "<align>left</align>" +
                    "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
      }

#endregion

      #region Summary Style 2

      if (sumStyle == mostRecentMovPicsSummaryStyle.fanart)
      {

        string mrMovieTitleFont = movPicsOptions.MovieTitleFont;
        string mrMovieDetailFont = movPicsOptions.MovieDetailFont;
        bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;



        string fanartProperty = "#StreamedMP.recentlyAdded.movie1.fanart";

        if (cbCycleFanart.Checked)
          fanartProperty = "#StreamedMP.MostRecentMovPicsImageFanart";

        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
              "<window>\n" +
                "<controls>\n" +
                  "<control>\n" +
                    "<description>GROUP: RecentlyAdded Movie 1</description>\n" +
                    "<type>group</type>\n" +
                    "<dimColor>0xffffffff</dimColor>\n" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+![string.starts(#StreamedMP.recentlyAdded.movie1.fanart,#)|string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)]</visible>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                    "<control>\n" +
                    "<description>Movie 1 BG</description>\n" +
                    "<posX>976</posX>\n" +
                    "<posY>50</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>306</width>\n" +
                    "<height>320</height>\n" +
                    "<texture>recentsummoverlaybg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Header label</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>70</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.LatestMovies</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>      " +
                  "<control>\n" +
                    "<description>Movie 1 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>245</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.title</label>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<font>" + mrMovieTitleFont + "</font>" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 1</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>262</posY>\n" +
                    "<width>257</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.runtime #StreamedMP.recentlyAdded.movie1.certification</label>\n" +
                    "<font>" + mrMovieDetailFont + "</font>" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Star Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1190</posX>" +
                    "<posY>262</posY>" +
                    "<width>70</width>" +
                    "<height>13</height>" +
                    "<texture>star#StreamedMP.recentlyAdded.movie1.score.png</texture>" +
                  "</control>" +
                  "<control>\n" +
                    "<description>Movie 1 thumb/fanart</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>92</posY>\n" +
                    "<width>268</width>\n" +
                    "<height>151</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>" + fanartProperty + "</texture>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Movie 2</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+![string.starts(#StreamedMP.recentlyAdded.movie2.fanart,#)|string.starts(#StreamedMP.recentlyAdded.movie2.thumb,#)]</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                  "<control>\n" +
                    "<description>Movie 2 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>280</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.title</label>\n" +
                    "<font>" + mrMovieTitleFont + "</font>" +
                    "<textcolor>White</textcolor>\n" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 2 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>297</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.runtime #StreamedMP.recentlyAdded.movie2.certification</label>\n" +
                    "<font>" + mrMovieDetailFont + "</font>" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>" +
                    "<description>Movie 1 Star Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1190</posX>" +
                    "<posY>297</posY>" +
                    "<width>70</width>" +
                    "<height>13</height>" +
                    "<texture>star#StreamedMP.recentlyAdded.movie2.score.png</texture>" +
                  "</control>" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Movie 3</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+![string.starts(#StreamedMP.recentlyAdded.movie3.fanart,#)|string.starts(#StreamedMP.recentlyAdded.movie3.thumb,#)]</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                  "<control>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>315</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie3.title</label>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<font>" + mrMovieTitleFont + "</font>" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 3 Additional</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>995</posX>\n" +
                    "<posY>332</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie3.runtime #StreamedMP.recentlyAdded.movie3.certification</label>\n" +
                    "<font>" + mrMovieDetailFont + "</font>" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>" +
                    "<description>Movie 3 Star Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1190</posX>" +
                    "<posY>332</posY>" +
                    "<width>70</width>" +
                    "<height>13</height>" +
                    "<texture>star#StreamedMP.recentlyAdded.movie3.score.png</texture>" +
                  "</control>" +
                "</control>\n" +
              "</controls>\n" +
            "</window>";
      }

      #endregion
    }
    #endregion

    #region Private Methods

    string mostRecentVisibleControls(isOverlayType isOverlay)
    {
      string visibleOn = null;
      if (isOverlay == isOverlayType.MovPics)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.movies)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
        }
        if (visibleOn != null)
          visibleOn += "]";
        return visibleOn;
      }
      else
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.tvSeries)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
        }
        if (visibleOn != null)
          visibleOn += "]";
        return visibleOn;
      }
    }

    #endregion
  }
}
