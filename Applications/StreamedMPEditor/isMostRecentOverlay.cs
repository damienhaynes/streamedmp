namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {
    public int baseXPosAdded;
    public int baseYPosAdded;
    public int baseXPosWatched;
    public int baseYPosWatched;

    public string latestMediaPrefix = "#fanarthandler";
    public bool lmh = false;

    #region Main

    void generateMostRecentOverlay(chosenMenuStyle menuStyle, isOverlayType isOverlay, int xPosAdded, int yPosAdded, int xPosWatched, int yPosWatched)
    {
      baseXPosAdded = xPosAdded;
      baseYPosAdded = yPosAdded;
      baseXPosWatched = xPosWatched;
      baseYPosWatched = yPosWatched;

      if (helper.pluginEnabled(Helper.Plugins.LatestMediaHandler))
      {
        latestMediaPrefix = "#latestMediaHandler";
        lmh = true;
      }

      switch (isOverlay)
      {
        case isOverlayType.TVSeries:
          doTVSeries(tvSeriesRecentStyle);
          break;
        case isOverlayType.MovPics:
          doMovingPictures(movPicsRecentStyle);
          break;
        case isOverlayType.Music:
          MostRecentMusicSummary();
          writeXMLFile("basichome.recentlyadded.Music.Summary.xml");
          break;
        case isOverlayType.RecordedTV:
          MostRecentRecordedTVSummary();
          writeXMLFile("basichome.recentlyadded.RecordedTV.Summary.xml");
          break;
        case isOverlayType.freeDriveSpace:
          driveFreeSpaceOverlay();
          writeXMLFile("basichome.FreeDriveSpace.Overlay.xml");
          break;
        case isOverlayType.sleepControl:
          sleepControlOverlay();
          writeXMLFile("basichome.SleepControl.Overlay.xml");
          break;
        case isOverlayType.stocks:
          stocksOverlay();
          writeXMLFile("basichome.Stocks.Overlay.xml");
          break;
        case isOverlayType.powerControl:
          powerControlOverlay();
          writeXMLFile("basichome.PowerControl.Overlay.xml");
          break;
        case isOverlayType.htpcInfo:
          htpcInfoOverlay();
          writeXMLFile("basichome.HTPCInfo.Overlay.xml");
          break;
        case isOverlayType.updateControl:
          updateControlOverlay();
          writeXMLFile("basichome.UpdateControl.Overlay.xml");
          break;
        case isOverlayType.myemailmanager:
          myeMailManagerOverlay();
          writeXMLFile("basichome.MyeMailManager.Overlay.xml");
          break;
        default:
          break;
      }
    }

    #endregion

    #region TVSeries Overlays

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
          baseXPosWatched = 5;
          baseYPosWatched = 50;
          horizontalTVSeriesSummary();
        }
        else
        {
          horizonalTVSeriesFull();
        }
      }
    }

    #endregion

    #region Movie Overlays

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
          baseXPosWatched = 5;
          baseYPosWatched = 50;
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
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series1.thumb,#)</visible>\n" +
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
                    "<texture>#StreamedMP.recentlyAdded.series1.thumb</texture>\n" +
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
                    "<label>#StreamedMP.recentlyAdded.series1.title</label>\n" +
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
                    "<label>#StreamedMP.recentlyAdded.series1.episodetitle</label>\n" +
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
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series1.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series1.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series2.thumb,#)</visible>\n" +
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
                    "<texture>#StreamedMP.recentlyAdded.series2.thumb</texture>\n" +
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
                    "<label>#StreamedMP.recentlyAdded.series2.title</label>\n" +
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
                    "<label>#StreamedMP.recentlyAdded.series2.episodetitle</label>\n" +
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
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series2.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series2.episodenumber</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Series</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series3.thumb,#)</visible>\n" +
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
                    "<texture>#StreamedMP.recentlyAdded.series3.thumb</texture>\n" +
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
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1020</posX>\n" +
                    "<posY>498</posY>\n" +
                    "<width>255</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series3.title</label>\n" +
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
                    "<label>#StreamedMP.recentlyAdded.series3.episodetitle</label>\n" +
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
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series3.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series3.episodenumber</label>\n" +
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
        if (cbTVSeriesRecentWatched.Checked)
          mostRecentTVSeriesWatched();

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

      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        overlayYpos -= 80;

      // Build the file
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Series</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series1.thumb,#)</visible>" +
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
                    "<texture>#StreamedMP.recentlyAdded.series1.thumb</texture>" +
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
                    "<label>#StreamedMP.recentlyAdded.series1.title</label>" +
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
                    "<label>#StreamedMP.recentlyAdded.series1.episodetitle</label>" +
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
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series1.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series1.episodenumber</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Series</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series2.thumb,#)</visible>" +
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
                    "<texture>#StreamedMP.recentlyAdded.series2.thumb</texture>" +
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
                    "<label>#StreamedMP.recentlyAdded.series2.title</label>" +
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
                    "<label>#StreamedMP.recentlyAdded.series2.episodetitle</label>" +
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
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series2.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series2.episodenumber</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                "</control>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Series</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series3.thumb,#)</visible>" +
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
                    "<texture>#StreamedMP.recentlyAdded.series3.thumb</texture>" +
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
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>981</posX>" +
                    "<posY>" + (overlayYpos + 29).ToString() + "</posY>" +
                    "<width>255</width>" +
                    "<label>#StreamedMP.recentlyAdded.series3.title</label>" +
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
                    "<label>#StreamedMP.recentlyAdded.series3.episodetitle</label>" +
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
                    "<label>#TVSeries.Translation.Season.Label: #StreamedMP.recentlyAdded.series3.season #TVSeries.Translation.Episode.Label: #StreamedMP.recentlyAdded.series3.episodenumber</label>" +
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

      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        menuTop -= 60;

      overlayYpos = menuTop - overlayOffset;
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.poster)
      {
        buildTVSeriesSummaryFile(overlayYpos, mostRecentTVSeriesSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.tvseries.HSum.xml");
      }
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.fanart)
      {
        buildTVSeriesSummaryFile(overlayYpos, mostRecentTVSeriesSummaryStyle.fanart);
        if (cbTVSeriesRecentWatched.Checked)
          mostRecentTVSeriesWatched();

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
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series3.thumb,#)</visible>" +
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
                      "<texture>#StreamedMP.recentlyAdded.series3.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>871</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#StreamedMP.recentlyAdded.series3.seasonx#StreamedMP.recentlyAdded.series3.episodenumber</label>" +
                      "<font>mediastream10tc</font>" +
                      "<align>center</align>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 2</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series2.thumb,#)</visible>" +
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
                      "<texture>#StreamedMP.recentlyAdded.series2.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1011</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#StreamedMP.recentlyAdded.series2.seasonx#StreamedMP.recentlyAdded.series2.episodenumber</label>" +
                      "<font>mediastream10tc</font>" +
                      "<align>center</align>" +
                      "<textcolor>White</textcolor>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 1</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series1.thumb,#)</visible>" +
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
                      "<texture>#StreamedMP.recentlyAdded.series1.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1151</posX>" +
                      "<posY>" + (overlayYpos + 194).ToString() + "</posY>" +
                      "<width>115</width>" +
                      "<label>#StreamedMP.recentlyAdded.series1.seasonx#StreamedMP.recentlyAdded.series1.episodenumber</label>" +
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
        string fanartProperty = "#StreamedMP.MostRecentImageFanart";
        string fadelabelControl = "fadelabel";
        string mrSeriesNameFont = tvSeriesOptions.mrSeriesFont;
        string mrEpisodeFont = tvSeriesOptions.mrEpisodeFont;
        bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;

        if (tvSeriesOptions.mrDisableFadeLabels)
          fadelabelControl = "label";

        if (!mostRecentTVSeriesCycleFanart)
          fanartProperty = "#StreamedMP.recentlyAdded.series1.fanart";

        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
              "<window>\n" +
                "<controls>\n" +
                  "<control>\n" +
                    "<description>GROUP: RecentlyAdded Series</description>\n" +
                    "<type>group</type>\n" +
                    "<dimColor>0xffffffff</dimColor>\n" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+string.equals(#StreamedMP.recentlyAdded.series1.show,true)</visible>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                    "<control>\n" +
                    "<description>Series 1 BG</description>\n" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>306</width>\n" +
                    "<height>320</height>\n" +
                    "<texture>recentsummoverlaybg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Header label</description>\n" +
                    "<type>" + fadelabelControl + "</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 20).ToString() + "</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.LatestEpisodes</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>      " +
                  "<control>\n" +
                    "<description>Series 1 name</description>\n" +
                    "<type>" + fadelabelControl + "</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 195).ToString() + "</posY>\n" +
                    "<width>258</width>\n" +
                    "<scrollStartDelaySec>30</scrollStartDelaySec>";
        if (mrSeriesTitleLast)
          xml += "<label>#StreamedMP.MostRecent.Added.1.SEFormat - #StreamedMP.recentlyAdded.series1.title</label>\n";
        else
          xml += "<label>#StreamedMP.recentlyAdded.series1.title - #StreamedMP.MostRecent.Added.1.SEFormat</label>\n";
        xml += "<font>" + mrSeriesNameFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>\n" +
      "<control>\n" +
        "<description>Series 1 title</description>\n" +
        "<type>" + fadelabelControl + "</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 212).ToString() + "</posY>\n" +
        "<width>255</width>\n" +
        "<scrollStartDelaySec>30</scrollStartDelaySec>" +
        "<label>#StreamedMP.recentlyAdded.series1.episodetitle</label>\n" +
        "<font>" + mrEpisodeFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>" +
      "<control>\n" +
        "<description>Series 1 thumb/fanart</description>\n" +
        "<type>image</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 42).ToString() + "</posY>\n" +
        "<width>268</width>\n" +
        "<height>151</height>\n" +
        "<keepaspectratio>true</keepaspectratio>\n" +
        "<texture>" + fanartProperty + "</texture>\n" +
        "<shouldCache>true</shouldCache>\n" +
      "</control>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>GROUP: RecentlyAdded Series</description>\n" +
      "<type>group</type>\n" +
      "<dimColor>0xffffffff</dimColor>\n" +
      "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+string.equals(#StreamedMP.recentlyAdded.series2.show,true)]</visible>" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
      "<control>\n" +
        "<description>Series 2 name</description>\n" +
        "<type>" + fadelabelControl + "</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 230).ToString() + "</posY>\n" +
        "<width>258</width>\n" +
        "<scrollStartDelaySec>30</scrollStartDelaySec>";
        if (mrSeriesTitleLast)
          xml += "<label>#StreamedMP.MostRecent.Added.2.SEFormat - #StreamedMP.recentlyAdded.series2.title</label>\n";
        else
          xml += "<label>#StreamedMP.recentlyAdded.series2.title - #StreamedMP.MostRecent.Added.2.SEFormat</label>\n";
        xml += "<font>" + mrSeriesNameFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>\n" +
      "<control>\n" +
        "<description>Series 2 title</description>\n" +
        "<type>" + fadelabelControl + "</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
        "<width>255</width>\n" +
        "<scrollStartDelaySec>30</scrollStartDelaySec>" +
        "<label>#StreamedMP.recentlyAdded.series2.episodetitle</label>\n" +
        "<font>" + mrEpisodeFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>GROUP: RecentlyAdded Series</description>\n" +
      "<type>group</type>\n" +
      "<dimColor>0xffffffff</dimColor>\n" +
      "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+string.equals(#StreamedMP.recentlyAdded.series3.show,true)</visible>" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
      "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
      "<control>\n" +
        "<type>" + fadelabelControl + "</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 265).ToString() + "</posY>\n" +
        "<width>258</width>\n" +
        "<scrollStartDelaySec>30</scrollStartDelaySec>";
        if (mrSeriesTitleLast)
          xml += "<label>#StreamedMP.MostRecent.Added.3.SEFormat - #StreamedMP.recentlyAdded.series3.title</label>\n";
        else
          xml += "<label>#StreamedMP.recentlyAdded.series3.title - #StreamedMP.MostRecent.Added.3.SEFormat</label>\n";
        xml += "<font>" + mrSeriesNameFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>\n" +
      "<control>\n" +
        "<description>Series 3 title</description>\n" +
        "<type>" + fadelabelControl + "</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 282).ToString() + "</posY>\n" +
        "<width>255</width>\n" +
        "<scrollStartDelaySec>30</scrollStartDelaySec>" +
        "<label>#StreamedMP.recentlyAdded.series3.episodetitle</label>\n" +
        "<font>" + mrEpisodeFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>\n" +
    "</control>\n";
        if (!cbTVSeriesRecentWatched.Checked)
        {
          xml += "</controls>\n" +
         "</window>";
        }
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
        if (cbMovPicsRecentWatched.Checked)
          mostRecentMoviesWatched();

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

      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        overlayYpos -= 80;

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
                    "<label>#StreamedMP.recentlyAdded.movie3.runtime</label>" +
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

      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        menuTop -= 60;

      overlayYpos = menuTop - overlayOffset;
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.poster)
      {
        buildMovingPicturesSummaryFile(overlayYpos, mostRecentMovPicsSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.movpics.HSum.xml");
      }
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.fanart)
      {
        buildMovingPicturesSummaryFile(overlayYpos, mostRecentMovPicsSummaryStyle.fanart);
        if (cbMovPicsRecentWatched.Checked)
          mostRecentMoviesWatched();

        writeXMLFile("basichome.recentlyadded.movpics.HSum2.xml");
      }


    }

    #endregion

    #region MovingPictures Summary Builder

    void buildMovingPicturesSummaryFile(int sumStyle1OverlayYpos, mostRecentMovPicsSummaryStyle sumStyle)
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
                      "<posY>" + (sumStyle1OverlayYpos).ToString() + "</posY>" +
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
                      "<posY>" + (sumStyle1OverlayYpos + 11).ToString() + "</posY>" +
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
                    "<posY>" + (sumStyle1OverlayYpos + 185).ToString() + "</posY>" +
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
                      "<posY>" + (sumStyle1OverlayYpos).ToString() + "</posY>" +
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
                      "<posY>" + (sumStyle1OverlayYpos + 11).ToString() + "</posY>" +
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
                    "<posY>" + (sumStyle1OverlayYpos + 185).ToString() + "</posY>" +
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
                      "<posY>" + (sumStyle1OverlayYpos).ToString() + "</posY>" +
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
                      "<posY>" + (sumStyle1OverlayYpos + 11).ToString() + "</posY>" +
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
                      "<posY>" + (sumStyle1OverlayYpos + 185).ToString() + "</posY>" +
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
        bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;

        int xPos = baseXPosAdded + 20;

        string mrMovieTitleFont = movPicsOptions.MovieTitleFont;
        string mrMovieDetailFont = movPicsOptions.MovieDetailFont;
        string alignTxt = "right";
        string fanartProperty = "#StreamedMP.recentlyAdded.movie1.fanart";
        string fadeLabelControl = "fadelabel";

        if (movPicsOptions.DisableFadeLabels)
          fadeLabelControl = "label";

        if (cbCycleFanart.Checked)
          fanartProperty = "#StreamedMP.MostRecentMovPicsImageFanart";

        xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
              "<window>\n" +
                "<controls>\n" +
                  "<control>\n" +
                    "<description>GROUP: RecentlyAdded Movie 1</description>\n" +
                    "<type>group</type>\n" +
                    "<dimColor>0xffffffff</dimColor>\n" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+string.equals(#StreamedMP.recentlyAdded.movie1.show,true)</visible>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                    "<control>\n" +
                    "<description>Movie 1 BG</description>\n" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
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
                    "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 20).ToString() + "</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.LatestMovies</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>      " +
                  "<control>\n" +
                    "<description>Movie 1 Title</description>\n" +
                    "<type>" + fadeLabelControl + "</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 195).ToString() + "</posY>\n" +
                    "<width>258</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.title</label>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<font>" + mrMovieTitleFont + "</font>" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n";

        if (!movPicsOptions.HideRuntime)
        {
          xml += "<control>\n" +
            "<description>Movie 1 Runtime</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
             "<posY>" + (baseYPosAdded + 212).ToString() + "</posY>\n" +
             "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie1.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = 1180;
        }
        else
        {
          alignTxt = "left";
          xPos -= 5;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 1 Certification</description>\n" +
            "<type>fadelabel</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 212).ToString() + "</posY>\n" +
            "<width>100</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie1.certification</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
            "<align>" + alignTxt + "</align>\n" +
            "<scrollStartDelaySec>30</scrollStartDelaySec>\n" +
            "<wrapString> |  </wrapString>\n" +
          "</control>";
        }

        if (!movPicsOptions.HideRating)
        {
          if (movPicsOptions.UseTextRating)
          {
            xml += "<control>" +
              "<description>Movie 1 Star Rating Text</description>" +
              "<type>label</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 284).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 215).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<font>" + mrMovieDetailFont + "</font>" +
              "<align>right</align>" +
              "<label>#StreamedMP.recentlyAdded.movie1.actualscore/10</label>" +
            "</control>";
          }
          else
          {
            xml += "<control>" +
              "<description>Movie 1 Star Rating Image</description>" +
              "<type>image</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 214).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 215).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<texture>star#StreamedMP.recentlyAdded.movie1.score.png</texture>" +
            "</control>";
          }
        }

        xPos = baseXPosAdded + 20;
        xml += "<control>\n" +
                "<description>Movie 1 thumb/fanart</description>\n" +
                "<type>image</type>\n" +
                "<id>0</id>\n" +
                "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                "<posY>" + (baseYPosAdded + 42).ToString() + "</posY>\n" +
                "<width>268</width>\n" +
                "<height>151</height>\n" +
                "<keepaspectratio>true</keepaspectratio>\n" +
                "<texture>" + fanartProperty + "</texture>\n" +
                "<shouldCache>true</shouldCache>\n" +
              "</control>\n" +
            "</control>\n" +
            "<control>\n" +
              "<description>GROUP: RecentlyAdded Movie 2</description>\n" +
              "<type>group</type>\n" +
              "<dimColor>0xffffffff</dimColor>\n" +
              "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+string.equals(#StreamedMP.recentlyAdded.movie2.show,true)</visible>" +
              "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
              "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
              "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
              "<control>\n" +
                "<description>Movie 2 Title</description>\n" +
                "<type>" + fadeLabelControl + "</type>\n" +
                "<id>0</id>\n" +
                "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                "<posY>" + (baseYPosAdded + 230).ToString() + "</posY>\n" +
                "<width>258</width>\n" +
                "<label>#StreamedMP.recentlyAdded.movie2.title</label>\n" +
                "<font>" + mrMovieTitleFont + "</font>" +
                "<textcolor>White</textcolor>\n" +
                "<scrollStartDelaySec>20</scrollStartDelaySec>" +
              "</control>\n";

        if (!movPicsOptions.HideRuntime)
        {
          xml += "<control>\n" +
            "<description>Movie 2 Runtime</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = baseXPosAdded + 204;
        }
        else
        {
          xPos -= 5;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 2 Certification</description>\n" +
            "<type>fadelabel</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
            "<width>100</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie2.certification</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
            "<align>" + alignTxt + "</align>\n" +
            "<scrollStartDelaySec>30</scrollStartDelaySec>\n" +
            "<wrapString> |  </wrapString>\n" +
          "</control>";
        }

        if (!movPicsOptions.HideRating)
        {
          if (movPicsOptions.UseTextRating)
          {
            xml += "<control>" +
              "<description>Movie 2 Star Rating Text</description>" +
              "<type>label</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 284).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<font>" + mrMovieDetailFont + "</font>" +
              "<align>right</align>" +
              "<label>#StreamedMP.recentlyAdded.movie2.actualscore/10</label>" +
            "</control>";
          }
          else
          {
            xml += "<control>" +
              "<description>Movie 2 Star Rating Image</description>" +
              "<type>image</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 214).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 251).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<texture>star#StreamedMP.recentlyAdded.movie2.score.png</texture>" +
            "</control>";
          }
        }

        xPos = baseXPosAdded + 20;
        xml += "</control>\n" +
        "<control>\n" +
          "<description>GROUP: RecentlyAdded Movie 3</description>\n" +
          "<type>group</type>\n" +
          "<dimColor>0xffffffff</dimColor>\n" +
          "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+string.equals(#StreamedMP.recentlyAdded.movie3.show,true)</visible>" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
          "<control>\n" +
            "<type>" + fadeLabelControl + "</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 265).ToString() + "</posY>\n" +
            "<width>258</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie3.title</label>\n" +
            "<textcolor>White</textcolor>\n" +
            "<font>" + mrMovieTitleFont + "</font>" +
            "<scrollStartDelaySec>20</scrollStartDelaySec>" +
          "</control>\n";


        if (!movPicsOptions.HideRuntime)
        {
          xml += "<control>\n" +
            "<description>Movie 3 Runtime</description>\n" +
            "<type>label</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 282).ToString() + "</posY>\n" +
            "<width>257</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie3.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = baseXPosAdded + 204;
        }
        else
        {
          xPos -= 5;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 3 Certification</description>\n" +
            "<type>fadelabel</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 282).ToString() + "</posY>\n" +
            "<width>100</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie3.certification</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
            "<align>" + alignTxt + "</align>\n" +
            "<scrollStartDelaySec>30</scrollStartDelaySec>\n" +
            "<wrapString> |  </wrapString>\n" +
          "</control>";
        }

        if (!movPicsOptions.HideRating)
        {
          if (movPicsOptions.UseTextRating)
          {
            xml += "<control>" +
              "<description>Movie 3 Star Rating Text</description>" +
              "<type>label</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 284).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 282).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<font>" + mrMovieDetailFont + "</font>" +
              "<align>right</align>" +
              "<label>#StreamedMP.recentlyAdded.movie3.actualscore/10</label>" +
            "</control>";
          }
          else
          {
            xml += "<control>" +
              "<description>Movie 3 Star Rating Image</description>" +
              "<type>image</type>" +
              "<id>0</id>" +
              "<posX>" + (baseXPosAdded + 214).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 285).ToString() + "</posY>\n" +
              "<width>70</width>" +
              "<height>13</height>" +
              "<texture>star#StreamedMP.recentlyAdded.movie3.score.png</texture>" +
            "</control>";
          }
        }

        xml += "</control>\n";
        if (!cbMovPicsRecentWatched.Checked)
        {
          xml += "</controls>\n" +
         "</window>";
        }
      }

      #endregion
    }

    #endregion

    #region MovingPictures Most Recent Watched

    void mostRecentMoviesWatched()
    {
      string fanartProperty = "#StreamedMP.recentlyWatched.movie1.fanart";
      string fadelabelControl = "fadelabel";
      string mediaControl = string.Empty;
      string alignTxt = "right";

      string mrMovieTitleFont = movPicsOptions.MovieTitleFont;
      string mrMovieDetailFont = movPicsOptions.MovieDetailFont; bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;
      int xPos = baseXPosWatched + 20; ;

      if (menuStyle == chosenMenuStyle.verticalStyle)
        mediaControl = "![player.hasmedia]+";

      if (movPicsOptions.DisableFadeLabels)
        fadelabelControl = "label";

      if (cbCycleFanart.Checked)
        fanartProperty = "#StreamedMP.MostRecentMovPicsImageFanartWatched";

      xml += "<control>\n" +
                  "<description>GROUP: RecentlyWatched Movie 1</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.MovPics) + "+string.equals(#StreamedMP.recentlyWatched.movie1.show,true)</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
                "<description>Movie 1 BG</description>\n" +
                  "<posX>" + baseXPosWatched.ToString() + "</posX>\n" +
                  "<posY>" + baseYPosWatched.ToString() + "</posY>\n" +
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
                  "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosWatched + 20).ToString() + "</posY>\n" +
                  "<width>258</width>\n" +
                  "<label>#StreamedMP.RecentlyWatched</label>\n" +
                  "<font>mediastream10tc</font>\n" +
                  "<textcolor>White</textcolor>\n" +
                "</control>      " +
                "<control>\n" +
                  "<description>Movie 1 Title</description>\n" +
                  "<type>" + fadelabelControl + "</type>\n" +
                  "<id>0</id>\n" +
                  "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosWatched + 195).ToString() + "</posY>\n" +
                  "<width>258</width>\n" +
                  "<label>#StreamedMP.recentlyWatched.movie1.title</label>\n" +
                  "<textcolor>White</textcolor>\n" +
                  "<font>" + mrMovieTitleFont + "</font>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>\n";

      if (!movPicsOptions.HideRuntime)
      {
        xml += "<control>\n" +
          "<description>Movie 1 Runtime</description>\n" +
          "<type>label</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 212).ToString() + "</posY>\n" +
          "<width>257</width>\n" +
          "<label>#StreamedMP.recentlyWatched.movie1.runtime</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
        "</control>";
        xPos = baseXPosWatched + 204;
      }
      else
      {
        alignTxt = "left";
        xPos -= 5;
      }

      if (!movPicsOptions.HideCertification)
      {
        xml += "<control>\n" +
          "<description>Movie 1 Certification</description>\n" +
          "<type>fadelabel</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 212).ToString() + "</posY>\n" +
          "<width>100</width>\n" +
          "<label>#StreamedMP.recentlyWatched.movie1.certification</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
          "<align>" + alignTxt + "</align>\n" +
          "<scrollStartDelaySec>30</scrollStartDelaySec>\n" +
          "<wrapString> |  </wrapString>\n" +
        "</control>";
      }

      if (!movPicsOptions.HideRating)
      {
        if (movPicsOptions.UseTextRating)
        {
          xml += "<control>" +
            "<description>Movie 1 Star Rating Text</description>" +
            "<type>label</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 284).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 215).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<align>right</align>" +
            "<label>#StreamedMP.recentlyWatched.movie1.actualscore/10</label>" +
          "</control>";
        }
        else
        {
          xml += "<control>" +
            "<description>Movie 1 Star Rating Image</description>" +
            "<type>image</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 214).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 215).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<texture>star#StreamedMP.recentlyWatched.movie1.score.png</texture>" +
          "</control>";
        }
      }

      xPos = baseXPosWatched + 19;
      xml += "<control>\n" +
              "<description>Movie 1 thumb/fanart</description>\n" +
              "<type>image</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 42).ToString() + "</posY>\n" +
              "<width>268</width>\n" +
              "<height>151</height>\n" +
              "<keepaspectratio>true</keepaspectratio>\n" +
              "<texture>" + fanartProperty + "</texture>\n" +
              "<shouldCache>true</shouldCache>\n" +
            "</control>\n" +
          "</control>\n" +
          "<control>\n" +
            "<description>GROUP: recentlyWatched Movie 2</description>\n" +
            "<type>group</type>\n" +
            "<dimColor>0xffffffff</dimColor>\n" +
            "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.MovPics) + "+string.equals(#StreamedMP.recentlyWatched.movie2.show,true)</visible>" +
            "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
            "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
            "<description>Movie 2 Title</description>\n" +
              "<type>" + fadelabelControl + "</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 230).ToString() + "</posY>\n" +
              "<width>258</width>\n" +
              "<label>#StreamedMP.recentlyWatched.movie2.title</label>\n" +
              "<font>" + mrMovieTitleFont + "</font>" +
              "<textcolor>White</textcolor>\n" +
              "<scrollStartDelaySec>20</scrollStartDelaySec>" +
            "</control>\n";

      if (!movPicsOptions.HideRuntime)
      {
        xml += "<control>\n" +
          "<description>Movie 2 Runtime</description>\n" +
          "<type>label</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
          "<width>257</width>\n" +
          "<label>#StreamedMP.recentlyWatched.movie2.runtime</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
        "</control>";
        xPos = baseXPosWatched + 204;
      }
      else
      {
        xPos -= 5;
      }

      if (!movPicsOptions.HideCertification)
      {
        xml += "<control>\n" +
          "<description>Movie 2 Certification</description>\n" +
          "<type>fadelabel</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
          "<width>100</width>\n" +
          "<label>#StreamedMP.recentlyWatched.movie2.certification</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
          "<align>" + alignTxt + "</align>\n" +
          "<scrollStartDelaySec>30</scrollStartDelaySec>\n" +
          "<wrapString> |  </wrapString>\n" +
        "</control>";
      }

      if (!movPicsOptions.HideRating)
      {
        if (movPicsOptions.UseTextRating)
        {
          xml += "<control>" +
            "<description>Movie 2 Star Rating Text</description>" +
            "<type>label</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 284).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<align>right</align>" +
            "<label>#StreamedMP.recentlyWatched.movie2.actualscore/10</label>" +
          "</control>";
        }
        else
        {
          xml += "<control>" +
            "<description>Movie 2 Star Rating Image</description>" +
            "<type>image</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 214).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 251).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<texture>star#StreamedMP.recentlyWatched.movie2.score.png</texture>" +
          "</control>";
        }
      }

      xPos = baseXPosWatched + 19;
      xml += "</control>\n" +
      "<control>\n" +
        "<description>GROUP: RecentlyAdded Movie 3</description>\n" +
        "<type>group</type>\n" +
        "<dimColor>0xffffffff</dimColor>\n" +
        "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.MovPics) + "+string.equals(#StreamedMP.recentlyWatched.movie3.show,true)</visible>" +
        "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
        "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
          "<type>" + fadelabelControl + "</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 265).ToString() + "</posY>\n" +
          "<width>258</width>\n" +
          "<label>#StreamedMP.recentlyWatched.movie3.title</label>\n" +
          "<textcolor>White</textcolor>\n" +
          "<font>" + mrMovieTitleFont + "</font>" +
          "<scrollStartDelaySec>20</scrollStartDelaySec>" +
        "</control>\n";


      if (!movPicsOptions.HideRuntime)
      {
        xml += "<control>\n" +
          "<description>Movie 3 Runtime</description>\n" +
          "<type>label</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
          "<width>257</width>\n" +
          "<label>#StreamedMP.recentlyWatched.movie3.runtime</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
        "</control>";
        xPos = baseXPosWatched + 204;
      }
      else
      {
        xPos -= 5;
      }

      if (!movPicsOptions.HideCertification)
      {
        xml += "<control>\n" +
          "<description>Movie 3 Certification</description>\n" +
          "<type>fadelabel</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
          "<width>100</width>\n" +
          "<label>#StreamedMP.recentlyWatched.movie3.certification</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
          "<align>" + alignTxt + "</align>\n" +
          "<scrollStartDelaySec>30</scrollStartDelaySec>\n" +
          "<wrapString> |  </wrapString>\n" +
        "</control>";
      }

      if (!movPicsOptions.HideRating)
      {
        if (movPicsOptions.UseTextRating)
        {
          xml += "<control>" +
            "<description>Movie 3 Star Rating Text</description>" +
            "<type>label</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 284).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<align>right</align>" +
            "<label>#StreamedMP.recentlyWatched.movie3.actualscore/10</label>" +
          "</control>";
        }
        else
        {
          xml += "<control>" +
            "<description>Movie 3 Star Rating Image</description>" +
            "<type>image</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 214).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 285).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<texture>star#StreamedMP.recentlyWatched.movie3.score.png</texture>" +
          "</control>";
        }
      }

      xml += "</control>\n" +
    "</controls>\n" +
  "</window>";
    }

    #endregion

    #region TVSeries Most Recent Watched

    void mostRecentTVSeriesWatched()
    {
      string fanartProperty = "#StreamedMP.MostRecentImageFanartWatched";
      string fadeLabelControl = "fadelabel";
      string mediaControl = string.Empty;

      string mrSeriesNameFont = tvSeriesOptions.mrSeriesFont;
      string mrEpisodeFont = tvSeriesOptions.mrEpisodeFont;
      bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;

      if (menuStyle == chosenMenuStyle.verticalStyle)
        mediaControl = "[!player.hasmedia]+";

      if (tvSeriesOptions.mrDisableFadeLabels)
        fadeLabelControl = "label";

      if (!mostRecentTVSeriesCycleFanart)
        fanartProperty = "#StreamedMP.recentlyWatched.series1.fanart";

      xml += "<control>\n" +
                "<description>GROUP: RecentlyWatched Series</description>\n" +
                "<type>group</type>\n" +
                "<dimColor>0xffffffff</dimColor>\n" +
                "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.TVSeries) + "+string.equals(#StreamedMP.recentlyWatched.series1.show,true)</visible>" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
              "<description>Series 1 BG</description>\n" +
              "<posX>" + baseXPosWatched.ToString() + "</posX>\n" +
              "<posY>" + baseYPosWatched.ToString() + "</posY>\n" +
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
              "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 20).ToString() + "</posY>\n" +
              "<width>258</width>\n" +
              "<label>#StreamedMP.RecentlyWatched</label>\n" +
              "<font>mediastream10tc</font>\n" +
              "<textcolor>White</textcolor>\n" +
            "</control>      " +
            "<control>\n" +
              "<description>Series 1 name</description>\n" +
              "<type>" + fadeLabelControl + "</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 195).ToString() + "</posY>\n" +
              "<width>258</width>\n" +
              "<scrollStartDelaySec>30</scrollStartDelaySec>";
      if (mrSeriesTitleLast)
        xml += "<label>#StreamedMP.MostRecent.Watched.1.SEFormat - #StreamedMP.recentlyWatched.series1.title</label>\n";
      else
        xml += "<label>#StreamedMP.recentlyWatched.series1.title - #StreamedMP.MostRecent.Watched.1.SEFormat</label>\n";
      xml += "<font>" + mrSeriesNameFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>Series 1 title</description>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 212).ToString() + "</posY>\n" +
      "<width>255</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>" +
      "<label>#StreamedMP.recentlyWatched.series1.episodetitle</label>\n" +
      "<font>" + mrEpisodeFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>" +
    "<control>\n" +
      "<description>Series 1 thumb/fanart</description>\n" +
      "<type>image</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 42).ToString() + "</posY>\n" +
      "<width>268</width>\n" +
      "<height>151</height>\n" +
      "<keepaspectratio>true</keepaspectratio>\n" +
      "<texture>" + fanartProperty + "</texture>\n" +
      "<shouldCache>true</shouldCache>\n" +
    "</control>\n" +
  "</control>\n" +
  "<control>\n" +
    "<description>GROUP: RecentlyWatched Series</description>\n" +
    "<type>group</type>\n" +
    "<dimColor>0xffffffff</dimColor>\n" +
    "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.TVSeries) + "+string.equals(#StreamedMP.recentlyWatched.series2.show,true)</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
      "<description>Series 2 name</description>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 230).ToString() + "</posY>\n" +
      "<width>258</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>";
      if (mrSeriesTitleLast)
        xml += "<label>#StreamedMP.MostRecent.Watched.2.SEFormat - #StreamedMP.recentlyWatched.series2.title</label>\n";
      else
        xml += "<label>#StreamedMP.recentlyWatched.series2.title - #StreamedMP.MostRecent.Watched.2.SEFormat</label>\n";
      xml += "<font>" + mrSeriesNameFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>Series 2 title</description>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
      "<width>255</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>" +
      "<label>#StreamedMP.recentlyWatched.series2.episodetitle</label>\n" +
      "<font>" + mrEpisodeFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
  "</control>\n" +
  "<control>\n" +
    "<description>GROUP: RecentlyWatched Series</description>\n" +
    "<type>group</type>\n" +
    "<dimColor>0xffffffff</dimColor>\n" +
    "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.TVSeries) + "+string.equals(#StreamedMP.recentlyWatched.series3.show,true)</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 265).ToString() + "</posY>\n" +
      "<width>258</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>";
      if (mrSeriesTitleLast)
        xml += "<label>#StreamedMP.MostRecent.Watched.3.SEFormat - #StreamedMP.recentlyWatched.series3.title</label>\n";
      else
        xml += "<label>#StreamedMP.recentlyWatched.series3.title - #StreamedMP.MostRecent.Watched.3.SEFormat</label>\n";
      xml += "<font>" + mrSeriesNameFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>Series 3 title</description>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
      "<width>255</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>" +
      "<label>#StreamedMP.recentlyWatched.series3.episodetitle</label>\n" +
      "<font>" + mrEpisodeFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
  "</control>\n" +
  "</controls>\n" +
  "</window>";
    }

    #endregion

    #region Most Recent Music

    void MostRecentMusicSummary()
    {
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
            "<window>\n" +
              "<controls>\n" +
                "<control>" +
                "<description>GROUP: RecentlyAdded Music 1</description>" +
                "<type>group</type>" +
                "<dimColor>0xffffffff</dimColor>";

         if (lmh)
           xml += "<visible>" + mostRecentVisibleControls(isOverlayType.Music) + "+[string.equals(#latestMediaHandler.music.latest.enabled,true)]" + "</visible>";
         else
         xml += "<visible>" + mostRecentVisibleControls(isOverlayType.Music) + "+![string.starts(#fanarthandler.music.latest1.artist,#)]" + "</visible>";

         xml += "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Music 1 BG</description>" +
                  "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                  "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<width>306</width>" +
                  "<height>320</height>" +
                  "<texture>recentsummoverlaybg.png</texture>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>" +
                "</control>" +
                "<control>" +
                  "<description>Header label</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 20).ToString() + "</posY>\n" +
                  "<width>258</width>" +
                  "<label>#StreamedMP.LatestMusic</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Music 1 Artist</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 195).ToString() + "</posY>\n" +
                  "<label>" + latestMediaPrefix + ".music.latest1.artist</label>" +
                  "<textcolor>White</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>Music 1 Album</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 212).ToString() + "</posY>\n" +
                  "<width>257</width>" +
                  "<label>" + latestMediaPrefix + ".music.latest1.album</label>" +
                  "<font>mediastream10c</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Music1 thumb/fanart</description>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 71).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 42).ToString() + "</posY>\n" +
                  "<width>155</width>" +
                  "<height>155</height>" +
                  "<keepaspectratio>true</keepaspectratio>" +
                  "<texture>" + latestMediaPrefix + ".music.latest1.thumb</texture>" +
                  "<shouldCache>true</shouldCache>\n" +
                "</control>" +
              "</control>" +
              "<control>" +
                "<description>GROUP: RecentlyAdded Music 2</description>" +
                "<type>group</type>" +
                "<dimColor>0xffffffff</dimColor>";

       if (lmh)
         xml += "<visible>" + mostRecentVisibleControls(isOverlayType.Music) + "+[string.equals(#latestMediaHandler.music.latest.enabled,true)]" + "</visible>";
       else
         xml += "<visible>" + mostRecentVisibleControls(isOverlayType.Music) + "+![string.starts(#fanarthandler.music.latest2.artist,#)]" + "</visible>";

         xml += "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Music 2 Artist</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 230).ToString() + "</posY>\n" +
                  "<width>258</width>" +
                  "<label>" + latestMediaPrefix + ".music.latest2.artist</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>Movie 2 Album</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
                  "<width>257</width>" +
                  "<label>" + latestMediaPrefix + ".music.latest2.album</label>" +
                  "<font>mediastream10c</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
              "</control>" +
              "<control>" +
                "<description>GROUP: RecentlyAdded Music 3</description>" +
                "<type>group</type>" +
                "<dimColor>0xffffffff</dimColor>";

       if (lmh)
         xml += "<visible>" + mostRecentVisibleControls(isOverlayType.Music) + "+[string.equals(#latestMediaHandler.music.latest.enabled,true)]" + "</visible>";
       else
         xml += "<visible>" + mostRecentVisibleControls(isOverlayType.Music) + "+![string.starts(#fanarthandler.music.latest3.artist,#)]" + "</visible>";

         xml += "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Music 3 Artist</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 265).ToString() + "</posY>\n" +
                  "<width>258</width>" +
                  "<label>" + latestMediaPrefix + ".music.latest3.artist</label>" +
                  "<textcolor>White</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>Music 3 Album</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 282).ToString() + "</posY>\n" +
                  "<width>257</width>" +
                  "<label>" + latestMediaPrefix + ".music.latest3.album</label>" +
                  "<font>mediastream10c</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
              "</control>" +
            "</controls>" +
          "</window>";
    }

    #endregion

    #region Most Recent RecordedTV

    void MostRecentRecordedTVSummary()
    {
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
            "<window>\n" +
              "<controls>\n" +
                "<control>" +
                "<description>GROUP: RecentlyAdded RecordedTV 1</description>" +
                "<type>group</type>" +
                "<dimColor>0xffffffff</dimColor>";

    if (lmh)
      xml += "<visible>" + mostRecentVisibleControls(isOverlayType.RecordedTV) + "+[string.equals(#latestMediaHandler.tvrecordings.latest.enabled,true)]" + "</visible>";
    else
      xml += "<visible>" + mostRecentVisibleControls(isOverlayType.RecordedTV) + "+![string.starts(#fanarthandler.tvrecordings.latest1.title,#)]" + "</visible>";

      xml += "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
             "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>RecordedTV 1 BG</description>" +
                  "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                  "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<width>306</width>" +
                  "<height>320</height>" +
                  "<texture>recentsummoverlaybg.png</texture>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV Header label</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 20).ToString() + "</posY>\n" +
                  "<width>258</width>" +
                  "<label>#StreamedMP.LatestTVRecordings</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV 1 title</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 195).ToString() + "</posY>\n" +
                  "<label>" + latestMediaPrefix + ".tvrecordings.latest1.title</label>" +
                  "<textcolor>White</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV 1 dateAdded</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 212).ToString() + "</posY>\n" +
                  "<width>257</width>" +
                  "<label>#StreamedMP.RecordedOn " + latestMediaPrefix + ".tvrecordings.latest1.dateAdded</label>" +
                  "<font>mediastream10c</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV thumb/fanart</description>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 42).ToString() + "</posY>\n" +
                  "<width>268</width>" +
                  "<height>151</height>" +
                  "<keepaspectratio>true</keepaspectratio>" +
                  "<texture>" + latestMediaPrefix + ".tvrecordings.latest1.thumb</texture>" +
                  "<shouldCache>true</shouldCache>\n" +
                "</control>" +
              "</control>" +
              "<control>" +
                "<description>GROUP: RecentlyAdded RecordedTV 2</description>" +
                "<type>group</type>" +
                "<dimColor>0xffffffff</dimColor>";

    if (lmh)
      xml += "<visible>" + mostRecentVisibleControls(isOverlayType.RecordedTV) + "+[string.equals(#latestMediaHandler.tvrecordings.latest.enabled,true)]" + "</visible>";
    else
      xml += "<visible>" + mostRecentVisibleControls(isOverlayType.RecordedTV) + "+![string.starts(#fanarthandler.tvrecordings.latest2.title,#)]" + "</visible>";

      xml += "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>RecordedTV 2 title</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 230).ToString() + "</posY>\n" +
                  "<width>258</width>" +
                  "<label>" + latestMediaPrefix + ".tvrecordings.latest2.title</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV 2 dateAdded</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
                  "<width>257</width>" +
                  "<label>#StreamedMP.RecordedOn " + latestMediaPrefix + ".tvrecordings.latest2.dateAdded</label>" +
                  "<font>mediastream10c</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
              "</control>" +
              "<control>" +
                "<description>GROUP: RecentlyAdded RecordedTV 3</description>" +
                "<type>group</type>" +
                "<dimColor>0xffffffff</dimColor>";

    if (lmh)
      xml += "<visible>" + mostRecentVisibleControls(isOverlayType.RecordedTV) + "+[string.equals(#latestMediaHandler.tvrecordings.latest.enabled,true)]" + "</visible>";
    else
      xml += "<visible>" + mostRecentVisibleControls(isOverlayType.RecordedTV) + "+![string.starts(#fanarthandler.tvrecordings.latest3.title,#)]" + "</visible>";

      xml += "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>RecordedTV 3 title</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 265).ToString() + "</posY>\n" +
                  "<width>258</width>" +
                  "<label>" + latestMediaPrefix + ".tvrecordings.latest3.title</label>" +
                  "<textcolor>White</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV 3 dateAdded</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 282).ToString() + "</posY>\n" +
                  "<width>257</width>" +
                  "<label>#StreamedMP.RecordedOn " + latestMediaPrefix + ".tvrecordings.latest3.dateAdded</label>" +
                  "<font>mediastream10c</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
              "</control>" +
            "</controls>" +
          "</window>";
    }

    #endregion

    #region DriveFreeSpace

    void driveFreeSpaceOverlay()
    {
      int yOffset = 10;
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
              "<window>" +
                  "<controls>" +
                      "<control>" +
                          "<description>GROUP: Power Control Plugins Overlay</description>" +
                          "<type>group</type>" +
                          "<dimColor>0xffffffff</dimColor>" +
                           "<visible>" + mostRecentVisibleControls(isOverlayType.freeDriveSpace) + "</visible>" +
                          "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                          "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                          "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                          "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                          "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                          "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                          "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                          "<control>" +
                          "<description>Overlay BG</description>" +
                          "<posX>976</posX>" +
                          "<posY>50</posY>" +
                          "<type>image</type>" +
                          "<id>0</id>" +
                          "<width>306</width>" +
                          "<height>320</height>" +
                          "<texture>recentsummoverlaybg.png</texture>" +
                          "<colordiffuse>EEFFFFFF</colordiffuse>" +
                          "</control>" +
                          "<control>" +
                          "<description>Plugin Name</description>" +
                          "<type>label</type>" +
                          "<id>0</id>" +
                          "<posX>995</posX>" +
                          "<posY>76</posY>" +
                          "<width>258</width>" +
                          "<label>Drive Free Space</label>" +
                          "<font>mediastream10tc</font>" +
                          "<textcolor>White</textcolor>" +
                          "</control>" +
                          "<control>" +
                          "<description>Index Separator</description>" +
                          "<type>label</type>" +
                          "<id>0</id>" +
                          "<posX>995</posX>" +
                          "<posY>80</posY>" +
                          "<width>264</width>" +
                          "<label>____________________________________________________________________________________________________________		</label>" +
                          "<textcolor>ff808080</textcolor>" +
                          "</control>";

      foreach (string driveToDisplay in driveFreeSpaceDrives)
      {

        string driveLetter = driveToDisplay.Substring(0, 1);

        string driveIcon = "FreeDriveSpace_Icon_C.png";
        if (driveLetter.ToLower() != "c")
          driveIcon = "FreeDriveSpace_Icon.png";

        xml += "<!-- *** Drive " + driveLetter + " *** -->" +
                  "<control>" +
                    "<description>Drive " + driveLetter + " Image</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>995</posX>" +
                    "<posY>" + (90 + yOffset).ToString() + "</posY>" +
                    "<width>60</width>" +
                    "<height>60</height>" +
                    "<texture>" + driveIcon + "</texture>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>Drive " + driveLetter + " Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>995</posX>" +
                    "<posY>" + (160 + yOffset).ToString() + "</posY>" +
                    "<width>266</width>" +
                    "<height>20</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>Drive " + driveLetter + " Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>985</posX>" +
                    "<posY>" + (161 + yOffset).ToString() + "</posY>" +
                    "<width>266</width>" +
                    "<height>20</height>" +
                    "<texturebg>-</texturebg>" +
                    "<label>#DriveFreeSpace." + driveLetter + ".AvailableSpace.UsedPercentage</label>" +
                    "<lefttexture>osdprogressleft.png</lefttexture>" +
                    "<midtexture>osdprogressmid.png</midtexture>" +
                    "<righttexture>osdprogressright</righttexture>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>Drive " + driveLetter + " Description</description>" +
                    "<type>label</type>" +
                    "<id>1</id>" +
                    "<posX>1060</posX>" +
                    "<posY>" + (100 + yOffset).ToString() + "</posY>" +
                    "<width>198</width>" +
                    "<label>#DriveFreeSpace." + driveLetter + ".AvailableSpace.Data</label>" +
                    "<font>mediastream10</font>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>";

        yOffset += 84;
      }

      xml += "</control>" +
"</controls>" +
"</window>";
    }

    #endregion

    #region SleepControl

    void sleepControlOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: Sleep Control Plugins Overlay</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>!string.starts(#SleepControl.Counter,#)+" + mostRecentVisibleControls(isOverlayType.sleepControl) + "</visible>" +
                  "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Overlay BG</description>" +
                  "<posX>976</posX>" +
                  "<posY>50</posY>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<width>306</width>" +
                  "<height>320</height>" +
                  "<texture>recentsummoverlaybg.png</texture>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>" +
                "</control>" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>76</posY>" +
                  "<width>258</width>" +
                  "<label>Sleep Control</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>80</posY>" +
                  "<width>264</width>				<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Mode Image</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1140</posX>" +
                  "<posY>74</posY>" +
                  "<width>20</width>" +
                  "<height>20</height>" +
                  "<texture>#SleepControl.Image</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Counter</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>976</posX>" +
                  "<posY>150</posY>" +
                  "<width>306</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Counter</label>" +
                  "<font>mediastream28tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Activity</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>976</posX>" +
                  "<posY>200</posY>" +
                  "<width>306</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Activity</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Mode</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>976</posX>" +
                  "<posY>230</posY>" +
                  "<width>306</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Method</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Start</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>976</posX>" +
                  "<posY>260</posY>" +
                  "<width>306</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Start</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control End</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>976</posX>" +
                  "<posY>290</posY>" +
                  "<width>306</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.End</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "</control>" +
              "</controls>" +
            "</window>";

    }
    #endregion

    #region Stocks and Indices

    void stocksOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: Power Control Plugins Overlay</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.stocks) + "</visible>" +
                  "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Overlay BG</description>" +
                  "<posX>976</posX>" +
                  "<posY>50</posY>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<width>306</width>" +
                  "<height>320</height>" +
                  "<texture>recentsummoverlaybg.png</texture>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>" +
                "</control>" +
                "<!-- *** Indices *** -->" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>76</posY>" +
                  "<width>258</width>" +
                  "<label>Indices</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>80</posY>" +
                  "<width>264</width>" +
                  "<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<!-- *** Index 0 *** -->" +
                "<control>" +
                  "<description>Stocks.Index0Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>100</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Index0Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index0Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>99</posY>" +
                  "<label>#Stocks.Index0Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index0Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>99</posY>" +
                  "<label>#Stocks.Index0Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index0PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>99</posY>" +
                  "<label>#Stocks.Index0PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Index 1 *** -->" +
                "<control>" +
                  "<description>Stocks.Index1Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>120</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Index1Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index1Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>119</posY>" +
                  "<label>#Stocks.Index1Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index1Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>119</posY>" +
                  "<label>#Stocks.Index1Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index1PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>119</posY>" +
                  "<label>#Stocks.Index1PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Index 2 *** -->" +
                "<control>" +
                  "<description>Stocks.Index2Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>140</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Index2Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index2Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>139</posY>" +
                  "<label>#Stocks.Index2Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index2Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>139</posY>" +
                  "<label>#Stocks.Index2Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index2PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>139</posY>" +
                  "<label>#Stocks.Index2PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Stocks *** -->" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>165</posY>" +
                  "<width>263</width>" +
                  "<label>Stocks</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>169</posY>" +
                  "<width>264</width>" +
                  "<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<!-- *** Stock 0 *** -->" +
                "<control>" +
                  "<description>Stocks.Stock0Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>189</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Stock0Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock0Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>188</posY>" +
                  "<label>#Stocks.Stock0Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock0Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>188</posY>" +
                  "<label>#Stocks.Stock0Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock0PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>188</posY>" +
                  "<label>#Stocks.Stock0PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Stock 1 *** -->" +
                "<control>" +
                  "<description>Stocks.Stock1Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>209</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Stock1Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock1Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>209</posY>" +
                  "<label>#Stocks.Stock1Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock1Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>209</posY>" +
                  "<label>#Stocks.Stock1Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock1PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>209</posY>" +
                  "<label>#Stocks.Stock1PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Stock 2 *** -->" +
                "<control>" +
                  "<description>Stocks.Stock2Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>229</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Stock2Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock2Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>228</posY>" +
                  "<label>#Stocks.Stock2Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock2Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>228</posY>" +
                  "<label>#Stocks.Stock2Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                    "<description>Stocks.Stock2PercentChange</description>" +
                    "<type>label</type>" +
                    "<id>1</id>" +
                    "<posX>1260</posX>" +
                    "<posY>228</posY>" +
                    "<label>#Stocks.Stock2PercentChange</label>" +
                    "<font>mediastream10c</font>" +
                    "<align>right</align>" +
                "</control>" +
                "<!-- *** Currencies *** -->" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>254</posY>" +
                  "<width>258</width>" +
                  "<label>Currencies</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>258</posY>" +
                  "<width>264</width>" +
                  "<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<!-- *** Currency 0 *** -->" +
                "<control>" +
                  "<description>Stocks.Currency0Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>278</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Currency0Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency0Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>190</width>" +
                  "<posX>1014</posX>" +
                  "<posY>277</posY>" +
                  "<label>#Stocks.Currency0Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency0Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>277</posY>" +
                  "<label>#Stocks.Currency0Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Currency 1 *** -->" +
                "<control>" +
                  "<description>Stocks.Currency1Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>298</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Currency1Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency1Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>190</width>" +
                  "<posX>1014</posX>" +
                  "<posY>297</posY>" +
                  "<label>#Stocks.Currency1Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency1Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>297</posY>" +
                  "<label>#Stocks.Currency1Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Currency 2 *** -->" +
                "<control>" +
                  "<description>Stocks.Currency2Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>318</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Currency2Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency2Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>190</width>" +
                  "<posX>1014</posX>" +
                  "<posY>317</posY>" +
                  "<label>#Stocks.Currency2Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency2Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>317</posY>" +
                  "<label>#Stocks.Currency2Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Refresh Time *** -->" +
                "<control>" +
                  "<description>Stocks.Time</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>76</posY>" +
                  "<label>#Stocks.Time</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                  "<visible>!string.starts(#Stocks.Time,#)</visible>" +
                "</control>" +
                "</control>" +
              "</controls>" +
            "</window>";
    }

    #endregion

    #region Power Control

    void powerControlOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: Power Control Plugins Overlay</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.powerControl) + "</visible>" +
                  "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                  "<control>" +
                    "<description>Movie 1 BG</description>" +
                    "<posX>976</posX>" +
                    "<posY>50</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>306</width>" +
                    "<height>320</height>" +
                    "<texture>recentsummoverlaybg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Header label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>995</posX>" +
                    "<posY>76</posY>" +
                    "<width>258</width>" +
                    "<label>Power Control</label>" +
                    "<font>mediastream10tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Index Separator</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>995</posX>" +
                    "<posY>80</posY>" +
                    "<width>264</width>" +
                    "<label>____________________________________________________________________________________________________________</label>" +
                    "<textcolor>ff808080</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Network Device Group 0</description>" +
                    "<type>group</type>" +
                    "<visible>!string.starts(#PowerControl.NetworkDevice0Description,Device)</visible>" +
                    "<!-- *** Network Device 0 *** -->" +
                    "<control>" +
                      "<description>Network Device 0 Image</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>995</posX>" +
                      "<posY>100</posY>" +
                      "<width>40</width>" +
                      "<height>40</height>" +
                      "<texture>#PowerControl.NetworkDevice0TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 0 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1042</posX>" +
                      "<posY>110</posY>" +
                      "<width>198</width>" +
                      "<label>#PowerControl.NetworkDevice0Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 0 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1240</posX>" +
                      "<posY>110</posY>>" +
                      "<texture>#PowerControl.NetworkDevice0AliveImage</texture>" +
                      "<width>20</width>" +
                      "<height>20</height>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>Network Device Group 1</description>" +
                    "<type>group</type>" +
                    "<visible>!string.starts(#PowerControl.NetworkDevice1Description,Device)</visible>" +
                    "<!-- *** Network Device 1 *** -->" +
                    "<control>" +
                      "<description>Network Device 1 Image</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>995</posX>" +
                      "<posY>152</posY>" +
                      "<width>40</width>" +
                      "<height>40</height>" +
                      "<texture>#PowerControl.NetworkDevice1TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 1 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1042</posX>" +
                      "<posY>162</posY>" +
                      "<label>#PowerControl.NetworkDevice1Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 1 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1240</posX>" +
                      "<posY>162</posY>>" +
                      "<texture>#PowerControl.NetworkDevice1AliveImage</texture>" +
                      "<width>20</width>" +
                      "<height>20</height>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                  "<description>Network Device Group 2</description>" +
                  "<type>group</type>" +
                  "<visible>!string.starts(#PowerControl.NetworkDevice2Description,Device)</visible>" +
                  "<!-- *** Network Device 2 *** -->" +
                    "<control>" +
                      "<description>Network Device 2 Image</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>995</posX>" +
                      "<posY>204</posY>" +
                      "<width>40</width>" +
                      "<height>40</height>" +
                      "<texture>#PowerControl.NetworkDevice2TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                        "<description>Network Device 2 Description</description>" +
                        "<type>label</type>" +
                        "<id>1</id>" +
                        "<posX>1042</posX>" +
                        "<posY>214</posY>" +
                        "<label>#PowerControl.NetworkDevice2Description</label>" +
                        "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 2 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1240</posX>" +
                      "<posY>214</posY>>" +
                      "<texture>#PowerControl.NetworkDevice2AliveImage</texture>" +
                      "<width>20</width>" +
                      "<height>20</height>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>Network Device Group 3</description>" +
                    "<type>group</type>" +
                    "<visible>!string.starts(#PowerControl.NetworkDevice3Description,Device)</visible>" +
                    "<!-- *** Network Device 3 *** -->" +
                    "<control>" +
                      "<description>Network Device 3 Image</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>995</posX>" +
                      "<posY>256</posY>" +
                      "<width>40</width>" +
                      "<height>40</height>" +
                      "<texture>#PowerControl.NetworkDevice3TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 3 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1042</posX>" +
                      "<posY>266</posY>" +
                      "<label>#PowerControl.NetworkDevice3Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 3 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1240</posX>" +
                      "<posY>266</posY>>" +
                      "<texture>#PowerControl.NetworkDevice3AliveImage</texture>" +
                      "<width>20</width>" +
                      "<height>20</height>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                  "<description>Network Device Group 4</description>" +
                  "<type>group</type>" +
                  "<visible>!string.starts(#PowerControl.NetworkDevice4Description,Device)</visible>" +
                    "<!-- *** Network Device 4 *** -->" +
                    "<control>" +
                      "<description>Network Device 4 Image</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>995</posX>" +
                      "<posY>308</posY>" +
                      "<width>40</width>" +
                      "<height>40</height>" +
                      "<texture>#PowerControl.NetworkDevice4TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 4 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1042</posX>" +
                      "<posY>318</posY>" +
                      "<label>#PowerControl.NetworkDevice4Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 4 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1240</posX>" +
                      "<posY>318</posY>" +
                      "<texture>#PowerControl.NetworkDevice4AliveImage</texture>" +
                      "<width>20</width>" +
                      "<height>20</height>" +
                    "</control>" +
                  "</control>" +
                "</control>" +
              "</controls>" +
            "</window>";
    }

    #endregion

    #region HTPCInfo Overlay

    void htpcInfoOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: Power Control Plugins Overlay</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.htpcInfo) + "</visible>" +
                  "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                  "<control>" +
                    "<description>Overlay BG</description>" +
                    "<posX>976</posX>" +
                    "<posY>50</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>306</width>" +
                    "<height>320</height>" +
                    "<texture>recentsummoverlaybg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Plugin Name</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>995</posX>" +
                    "<posY>76</posY>" +
                    "<width>258</width>" +
                    "<label>HTPC Info</label>" +
                    "<font>mediastream10tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Index Separator</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>995</posX>" +
                    "<posY>80</posY>" +
                    "<width>264</width>" +
                    "<label>____________________________________________________________________________________________________________</label>" +
                    "<textcolor>ff808080</textcolor>" +
                  "</control>" +
                  "<!-- *** CPU Infos *** -->" +
                  "<control>" +
                    "<description>CPU Picture</description>" +
                    "<posX>995</posX>" +
                    "<posY>100</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>80</width>" +
                    "<height>80</height>" +
                    "<texture>HTPC_Info_CPU.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Temp label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>115</posY>" +
                    "<width>170</width>" +
                    "<label>CPU Temp:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Temp value</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>115</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.SensorTemperatureCPU</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.SensorTemperatureCPU,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Usage label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>135</posY>" +
                    "<width>170</width>" +
                    "<label>CPU Usage:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Processor Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>135</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.ProcessorUsage%</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.ProcessorUsage,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>1086</posX>" +
                    "<posY>154</posY>" +
                    "<width>176</width>" +
                    "<height>20</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>1074</posX>" +
                    "<posY>155</posY>" +
                    "<width>196</width>" +
                    "<height>20</height>" +
                    "<texturebg>-</texturebg>" +
                    "<label>#HTPCInfo.ProcessorUsage</label>" +
                    "<lefttexture>osdprogressleft.png</lefttexture>" +
                    "<midtexture>osdprogressmid.png</midtexture>" +
                    "<righttexture>osdprogressright</righttexture>" +
                    "<visible>!string.starts(#HTPCInfo.ProcessorUsage,#)</visible>" +
                  "</control>" +
                  "<!-- *** RAM Infos *** -->" +
                  "<control>" +
                    "<description>CPU Picture</description>" +
                    "<posX>995</posX>" +
                    "<posY>185</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>80</width>" +
                    "<height>80</height>" +
                    "<texture>HTPC_Info_RAM.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Free RAM label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>200</posY>" +
                    "<width>170</width>" +
                    "<label>Free RAM:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Free RAM Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>200</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.MemoryAvailable</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.MemoryAvailable,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>RAM Usage label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>220</posY>" +
                    "<width>170</width>" +
                    "<label>RAM Usage:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Ram Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>220</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.MemoryPercentUsed%</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.MemoryPercentUsed,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>RAM Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>1086</posX>" +
                    "<posY>239</posY>" +
                    "<width>176</width>" +
                    "<height>20</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>RAM Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>1074</posX>" +
                    "<posY>240</posY>" +
                    "<width>196</width>" +
                    "<height>20</height>" +
                    "<texturebg>-</texturebg>" +
                    "<label>#HTPCInfo.MemoryPercentUsed</label>" +
                    "<lefttexture>osdprogressleft.png</lefttexture>" +
                    "<midtexture>osdprogressmid.png</midtexture>" +
                    "<righttexture>osdprogressright</righttexture>" +
                    "<visible>!string.starts(#HTPCInfo.MemoryPercentUsed,#)</visible>" +
                  "</control>" +
                  "<!-- *** GPU Infos *** -->" +
                  "<control>" +
                    "<description>CPU Picture</description>" +
                    "<posX>995</posX>" +
                    "<posY>275</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>80</width>" +
                    "<height>80</height>" +
                    "<texture>HTPC_Info_GPU.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Temp label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>290</posY>" +
                    "<width>170</width>" +
                    "<label>GPU Temp:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Temp value</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>290</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.GPUSensorTemperature0</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.GPUSensorTemperature0,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Usage label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>310</posY>" +
                    "<width>170</width>" +
                    "<label>GPU Usage:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>310</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.GPUSensorUsage0%</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.GPUSensorUsage0,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>1086</posX>" +
                    "<posY>329</posY>" +
                    "<width>176</width>" +
                    "<height>20</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>1074</posX>" +
                    "<posY>330</posY>" +
                    "<width>196</width>" +
                    "<height>20</height>" +
                    "<texturebg>-</texturebg>" +
                    "<label>#HTPCInfo.GPUSensorUsage0</label>" +
                    "<lefttexture>osdprogressleft.png</lefttexture>" +
                    "<midtexture>osdprogressmid.png</midtexture>" +
                    "<righttexture>osdprogressright</righttexture>" +
                    "<visible>!string.starts(#HTPCInfo.GPUSensorUsage0,#)</visible>" +
                  "</control>" +
                "</control>" +
              "</controls>" +
            "</window>";
    }

    #endregion

    #region Update Overlay

    void updateControlOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
              "<window>" +
                  "<controls>" +
                      "<control>" +
                          "<description>GROUP: Update Control Plugins Overlay</description>" +
                          "<type>group</type>" +
                          "<dimColor>0xffffffff</dimColor>" +
                          "<visible>" + mostRecentVisibleControls(isOverlayType.updateControl) + "</visible>" +
                          "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                          "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                          "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                          "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                          "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                          "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                          "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                          "<control>" +
                              "<description>Overlay BG</description>" +
                              "<posX>976</posX>" +
                              "<posY>50</posY>" +
                              "<type>image</type>" +
                              "<id>0</id>" +
                              "<width>306</width>" +
                              "<height>320</height>" +
                              "<texture>recentsummoverlaybg.png</texture>" +
                              "<colordiffuse>EEFFFFFF</colordiffuse>" +
                          "</control>" +
                          "<control>" +
                              "<description>Plugin Name</description>" +
                              "<type>label</type>" +
                              "<id>0</id>" +
                              "<posX>995</posX>" +
                              "<posY>76</posY>" +
                              "<width>258</width>" +
                              "<label>Update Control</label>" +
                              "<font>mediastream10tc</font>" +
                              "<textcolor>White</textcolor>" +
                          "</control>" +
                          "<control>" +
                              "<description>Index Separator</description>" +
                              "<type>label</type>" +
                              "<id>0</id>" +
                              "<posX>995</posX>" +
                              "<posY>80</posY>" +
                              "<width>264</width>" +
                              "<label>____________________________________________________________________________________________________________</label>" +
                              "<textcolor>ff808080</textcolor>" +
                          "</control>" +
                          "<control>" +
                              "<description>Urgency Image</description>" +
                              "<type>image</type>" +
                              "<id>1</id>" +
                              "<posX>995</posX>" +
                              "<posY>115</posY>" +
                              "<width>60</width>" +
                              "<height>60</height>" +
                              "<texture>updatecontrol_empty.png</texture>" +
                              "<visible>string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>" +
                          "<control>" +
                              "<description>Urgency Image</description>" +
                              "<type>image</type>" +
                              "<id>1</id>" +
                              "<posX>995</posX>" +
                              "<posY>115</posY>" +
                              "<width>60</width>" +
                              "<height>60</height>" +
                              "<texture>#UpdateControl.AvailableUpdateUrgencyImage</texture>" +
                              "<visible>!string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>" +
                          "<control>" +
                            "<description>Text</description>" +
                            "<type>textbox</type>" +
                            "<id>1</id>" +
                            "<posX>1060</posX>" +
                            "<posY>120</posY>" +
                            "<width>198</width>" +
                            "<label>New Updates Available</label>" +
                            "<font>mediastream12tc</font>" +
                            "<visible>!string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>" +
                          "<control>" +
                            "<description>Text</description>" +
                            "<type>textbox</type>" +
                            "<id>1</id>" +
                            "<posX>1060</posX>" +
                            "<posY>120</posY>" +
                            "<width>198</width>" +
                            "<label>No Updates Available</label>" +
                            "<font>mediastream12tc</font>" +
                            "<visible>string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>     " +
                          "<control>" +
                            "<type>group</type>" +
                            "<visible>!string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                            "<control>" +
                            "<description>Text available windows updates</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>190</posY>" +
                            "<width>198</width>" +
                            "<label>Available Updates</label>" +
                            "<font>mediastream10tc</font>" +
                          "</control>" +
                          "<control>" +
                            "<description>available windows updates</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>207</posY>" +
                            "<width>198</width>" +
                            "<label>#UpdateControl.AvailableUpdateCount</label>" +
                            "<font>mediastream10</font>          " +
                            "</control>" +
                          "<control>" +
                            "<description>Text available windows updates Size</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>235</posY>" +
                            "<width>198</width>" +
                            "<label>Size</label>" +
                            "<font>mediastream10tc</font>" +
                          "</control>" +
                          "<control>" +
                            "<description>available windows updates Size</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>252</posY>" +
                            "<width>198</width>" +
                            "<label>#UpdateControl.AvailableUpdateSize</label>" +
                            "<font>mediastream10</font>          " +
                          "</control>" +
                          "<control>" +
                            "<description>Text Update Time</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>280</posY>" +
                            "<width>198</width>" +
                            "<label>Update Time</label>" +
                            "<font>mediastream10tc</font>" +
                          "</control>" +
                          "<control>" +
                            "<description>Update.Time</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>297</posY>" +
                            "<label>#UpdateControl.UpdateDate</label>" +
                            "<font>mediastream10c</font>" +
                          "</control>" +
                          "</control>" +
                      "</control>" +
                  "</controls>" +
              "</window>";
    }

    #endregion

    #region MyeMailManager


    void myeMailManagerOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
              "<window>" +
                "<controls>" +
                    "<control>" +
                      "<description>Email Background</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>135</posX>" +
                      "<posY>10</posY>" +
                      "<width>210</width>" +
                      "<height>32</height>" +
                      "<texture>emailbackground.png</texture>" +
                      "<animation effect=\"fade\" time=\"200\" delay=\"100\">WindowOpen</animation>" +
                    "</control>" +
                    "<control>" +
                      "<description>New Email</description>" +
                      "<type>label</type>" +
                      "<posX>174</posX>" +
                      "<posY>13</posY>" +
                      "<label>#newEmail</label>" +
                      "<font>mediastream11</font>" +
                      "<textcolor>ffffffff</textcolor>" +
                      "<animation effect=\"fade\" time=\"200\" delay=\"100\">WindowOpen</animation>" +
                    "</control>" +
                  "</controls>" +
                "</window>";
    }

    #endregion

    #region Private Methods

    string mostRecentVisibleControls(isOverlayType isOverlay)
    {
      string visibleOn = null;
      //
      //Controls to display recent movies overlay
      //
      if (isOverlay == isOverlayType.MovPics)
      {
        foreach (menuItem menItem in menuItems)
        {
          if (menItem.showMostRecent == displayMostRecent.movies)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + menItem.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + menItem.id.ToString() + ")";
          }

          // Check Submenu Level 1
          if (menItem.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < menItem.subMenuLevel1.Count; i++)
            {
              if (menItem.subMenuLevel1[i].showMostRecent == displayMostRecent.movies)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Submenu Level 2
          if (menItem.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < menItem.subMenuLevel2.Count; i++)
            {
              if (menItem.subMenuLevel2[i].showMostRecent == displayMostRecent.movies)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent TVSeries overlay
      //
      if (isOverlay == isOverlayType.TVSeries)
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
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.tvSeries)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.tvSeries)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Music overlay
      //
      if (isOverlay == isOverlayType.Music)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.music)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.music)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.music)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent RecordedTV overlay
      //
      if (isOverlay == isOverlayType.RecordedTV)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.recordedTV)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.recordedTV)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.recordedTV)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent SleepContol overlay
      //
      if (isOverlay == isOverlayType.sleepControl)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.sleepControl)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.sleepControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.sleepControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent DriveFreeSpace overlay
      //
      if (isOverlay == isOverlayType.freeDriveSpace)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.freeDriveSpace)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.freeDriveSpace)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.freeDriveSpace)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Stocks and Indices overlay
      //
      if (isOverlay == isOverlayType.stocks)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.stocks)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.stocks)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.stocks)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Power Control overlay
      //
      if (isOverlay == isOverlayType.powerControl)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.powerControl)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.powerControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.powerControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent HTPCInfo overlay
      //
      if (isOverlay == isOverlayType.htpcInfo)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.htpcInfo)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.htpcInfo)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.htpcInfo)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Stocks and Indices overlay
      //
      if (isOverlay == isOverlayType.stocks)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.stocks)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.stocks)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.stocks)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Update Control overlay
      //
      if (isOverlay == isOverlayType.updateControl)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.updateControl)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.updateControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.updateControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }

      if ((isOverlay == isOverlayType.Music ||
           isOverlay == isOverlayType.RecordedTV ||
           isOverlay == isOverlayType.freeDriveSpace ||
           isOverlay == isOverlayType.sleepControl ||
           isOverlay == isOverlayType.stocks ||
           isOverlay == isOverlayType.powerControl ||
           isOverlay == isOverlayType.htpcInfo ||
           isOverlay == isOverlayType.updateControl ||
           isOverlay == isOverlayType.freeDriveSpace) && visibleOn == null)
      {
        visibleOn = "No";
        return visibleOn;
      }

      if (visibleOn != null)
        visibleOn += "]";
      return visibleOn;
    }

    #endregion
  }
}
