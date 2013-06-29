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
        case isOverlayType.MusicVideos:
          MostRecentMusicVideosSummary();
          writeXMLFile("basichome.recentlyadded.MusicVideo.Summary.xml");
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
          baseXPosWatched = 8;
          baseYPosWatched = 75;
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
          baseXPosWatched = 8;
          baseYPosWatched = 75;
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
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 1 BG</description>\n" +
                    "<posX>1335</posX>\n" +
                    "<posY>105</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>578</width>\n" +
                    "<height>270</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1350</posX>\n" +
                    "<posY>119</posY>\n" +
                    "<width>168</width>\n" +
                    "<height>248</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.series1.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1338</posX>\n" +
                    "<posY>107</posY>\n" +
                    "<height>272</height>\n" +
                    "<width>194</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>147</posY>\n" +
                    "<width>373</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series1.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>227</posY>\n" +
                    "<width>383</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series1.episodetitle</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 1 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>309</posY>\n" +
                    "<width>383</width>\n" +
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
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 2 BG</description>\n" +
                    "<posX>1335</posX>\n" +
                    "<posY>405</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>578</width>\n" +
                    "<height>270</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1353</posX>\n" +
                    "<posY>419</posY>\n" +
                    "<width>168</width>\n" +
                    "<height>248</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.series2.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1338</posX>\n" +
                    "<posY>405</posY>\n" +
                    "<height>272</height>\n" +
                    "<width>194</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>449</posY>\n" +
                    "<width>373</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series2.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>528</posY>\n" +
                    "<width>383</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series2.episodetitle</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 2 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>608</posY>\n" +
                    "<width>387</width>\n" +
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
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Series 3 BG</description>\n" +
                    "<posX>1335</posX>\n" +
                    "<posY>705</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>578</width>\n" +
                    "<height>270</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1352</posX>\n" +
                    "<posY>717</posY>\n" +
                    "<width>168</width>\n" +
                    "<height>248</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.series3.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1338</posX>\n" +
                    "<posY>705</posY>\n" +
                    "<height>272</height>\n" +
                    "<width>194</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 name</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>747</posY>\n" +
                    "<width>373</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series3.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 title</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>827</posY>\n" +
                    "<width>383</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.series3.episodetitle</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Series 3 episode</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>906</posY>\n" +
                    "<width>383</width>\n" +
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
        buildTVSeriesSummaryFile(713, mostRecentTVSeriesSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.tvseries.VSum.xml");
      }
      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.fanart)
      {
        buildTVSeriesSummaryFile(713, mostRecentTVSeriesSummaryStyle.fanart);
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
      int maxYpos = 804;
      int overlayOffset = 276;
      int hdOffset = 0;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + 81;
      int menuBot = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 45;

      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
        menuBot += (basicHomeValues.subMenuHeight - 45);

      if (menuBot > maxYpos)
      {
        overlayYpos = menuTop - overlayOffset;
        hdOffset -= 81;
      }
      else
        overlayYpos = 804;

      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        overlayYpos -= 120;

      // Build the file
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Series</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "+!string.starts(#StreamedMP.recentlyAdded.series1.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,450" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Series 1 BG</description>" +
                    "<posX>42</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>578</width>" +
                    "<height>270</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>54</posX>" +
                    "<posY>" + (overlayYpos + 15).ToString() + "</posY>" +
                    "<height>245</height>" +
                    "<width>167</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.series1.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>39</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<height>266</height>" +
                    "<width>192</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>238</posX>" +
                    "<posY>" + (overlayYpos + 44).ToString() + "</posY>" +
                    "<width>377</width>" +
                    "<label>#StreamedMP.recentlyAdded.series1.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 title</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>238</posX>" +
                    "<posY>" + (overlayYpos + 123).ToString() + "</posY>" +
                    "<width>377</width>" +
                    "<label>#StreamedMP.recentlyAdded.series1.episodetitle</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 1 episode</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>238</posX>" +
                    "<posY>" + (overlayYpos + 209).ToString() + "</posY>" +
                    "<width>377</width>" +
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
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,450" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Series 2 BG</description>" +
                    "<posX>663</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>578</width>" +
                    "<height>270</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 2 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>675</posX>" +
                    "<posY>" + (overlayYpos + 15).ToString() + "</posY>" +
                    "<height>245</height>" +
                    "<width>167</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.series2.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 2 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>660</posX>" +
                    "<posY>" + (overlayYpos + 2).ToString() + "</posY>" +
                    "<height>266</height>" +
                    "<width>192</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                  "<control>" +
                    "<description>Series 2 name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>862</posX>" +
                    "<posY>" + (overlayYpos + 44).ToString() + "</posY>" +
                    "<width>377</width>" +
                    "<label>#StreamedMP.recentlyAdded.series2.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 2 title</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>862</posX>" +
                    "<posY>" + (overlayYpos + 123).ToString() + "</posY>" +
                    "<width>377</width>" +
                    "<label>#StreamedMP.recentlyAdded.series2.episodetitle</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 2 episode</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>862</posX>" +
                    "<posY>" + (overlayYpos + 209).ToString() + "</posY>" +
                    "<width>377</width>" +
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
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,450" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Series 3 BG</description>" +
                    "<posX>1283</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>578</width>" +
                    "<height>270</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1295</posX>" +
                    "<posY>" + (overlayYpos + 15).ToString() + "</posY>" +
                    "<height>245</height>" +
                    "<width>167</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.series3.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1280</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<height>266</height>" +
                    "<width>192</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>1482</posX>" +
                    "<posY>" + (overlayYpos + 44).ToString() + "</posY>" +
                    "<width>377</width>" +
                    "<label>#StreamedMP.recentlyAdded.series3.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>1482</posX>" +
                    "<posY>" + (overlayYpos + 123).ToString() + "</posY>" +
                    "<width>377</width>" +
                    "<label>#StreamedMP.recentlyAdded.series3.episodetitle</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Series 3 episode</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1482</posX>" +
                    "<posY>" + (overlayYpos + 209).ToString() + "</posY>" +
                    "<width>377</width>" +
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
      int overlayOffset = 336;
      int menuOffset = 75;
      if (menuStyle == chosenMenuStyle.horizontalContextStyle || horizontalContextLabels.Checked)
        menuOffset = 90;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + menuOffset;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 45;

      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        menuTop -= 90;

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
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1292</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>203</width>" +
                      "<height>330</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1307</posX>" +
                      "<posY>" + (overlayYpos + 17).ToString() + "</posY>" +
                      "<width>173</width>" +
                      "<height>255</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.series3.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1307</posX>" +
                      "<posY>" + (overlayYpos + 291).ToString() + "</posY>" +
                      "<width>173</width>" +
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
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1502</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>203</width>" +
                      "<height>330</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1517</posX>" +
                      "<posY>" + (overlayYpos + 17).ToString() + "</posY>" +
                      "<width>173</width>" +
                      "<height>255</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.series2.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1517</posX>" +
                      "<posY>" + (overlayYpos + 291).ToString() + "</posY>" +
                      "<width>173</width>" +
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
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1712</posX>" +
                      "<posY>" + (overlayYpos).ToString() + "</posY>" +
                      "<width>203</width>" +
                      "<height>330</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1727</posX>" +
                      "<posY>" + (overlayYpos + 17).ToString() + "</posY>" +
                      "<width>173</width>" +
                      "<height>255</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.series1.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 episode</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>1727</posX>" +
                      "<posY>" + (overlayYpos + 291).ToString() + "</posY>" +
                      "<width>173</width>" +
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
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                    "<control>\n" +
                    "<description>Series 1 BG</description>\n" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>459</width>\n" +
                    "<height>480</height>\n" +
                    "<texture>recentsummoverlaybg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Header label</description>\n" +
                    "<type>" + fadelabelControl + "</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                    "<width>387</width>\n" +
                    "<label>#StreamedMP.LatestEpisodes</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>      " +
                  "<control>\n" +
                    "<description>Series 1 name</description>\n" +
                    "<type>" + fadelabelControl + "</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 293).ToString() + "</posY>\n" +
                    "<width>387</width>\n" +
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
        "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 318).ToString() + "</posY>\n" +
        "<width>383</width>\n" +
        "<scrollStartDelaySec>30</scrollStartDelaySec>" +
        "<label>#StreamedMP.recentlyAdded.series1.episodetitle</label>\n" +
        "<font>" + mrEpisodeFont + "</font>\n" +
        "<textcolor>White</textcolor>\n" +
      "</control>" +
      "<control>\n" +
        "<description>Series 1 thumb/fanart</description>\n" +
        "<type>image</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 63).ToString() + "</posY>\n" +
        "<width>402</width>\n" +
        "<height>227</height>\n" +
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
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
      "<control>\n" +
        "<description>Series 2 name</description>\n" +
        "<type>" + fadelabelControl + "</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 345).ToString() + "</posY>\n" +
        "<width>387</width>\n" +
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
        "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 371).ToString() + "</posY>\n" +
        "<width>383</width>\n" +
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
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
      "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
      "<control>\n" +
        "<type>" + fadelabelControl + "</type>\n" +
        "<id>0</id>\n" +
        "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 398).ToString() + "</posY>\n" +
        "<width>387</width>\n" +
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
        "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
        "<posY>" + (baseYPosAdded + 423).ToString() + "</posY>\n" +
        "<width>383</width>\n" +
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
                  "<description>GROUP: RecentlyAdded Movies</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Movie 1 BG</description>\n" +
                    "<posX>1335</posX>\n" +
                    "<posY>105</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>578</width>\n" +
                    "<height>270</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 1 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1350</posX>\n" +
                    "<posY>119</posY>\n" +
                    "<width>168</width>\n" +
                    "<height>248</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.movie1.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 1 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1338</posX>\n" +
                    "<posY>107</posY>\n" +
                    "<height>272</height>\n" +
                    "<width>194</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 1 title</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>147</posY>\n" +
                    "<width>373</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 1 runtime</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>227</posY>\n" +
                    "<width>383</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.runtime</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 1 certification</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>309</posY>\n" +
                    "<width>383</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie1.certification</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Movies</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie2.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Movie 2 BG</description>\n" +
                    "<posX>1335</posX>\n" +
                    "<posY>405</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>578</width>\n" +
                    "<height>270</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 2 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1353</posX>\n" +
                    "<posY>419</posY>\n" +
                    "<width>168</width>\n" +
                    "<height>248</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.movie2.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 2 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1338</posX>\n" +
                    "<posY>405</posY>\n" +
                    "<height>272</height>\n" +
                    "<width>194</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 2 title</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>449</posY>\n" +
                    "<width>373</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 2 runtime</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>528</posY>\n" +
                    "<width>383</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 2 certification</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>608</posY>\n" +
                    "<width>387</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie2.certification</label>\n" +
                    "<font>mediastream10</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>GROUP: RecentlyAdded Movies</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie3.thumb,#)</visible>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
                  "<control>\n" +
                    "<description>Movie 3 BG</description>\n" +
                    "<posX>1335</posX>\n" +
                    "<posY>705</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>578</width>\n" +
                    "<height>270</height>\n" +
                    "<texture>recentfullbg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 3 thumb</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1352</posX>\n" +
                    "<posY>717</posY>\n" +
                    "<width>168</width>\n" +
                    "<height>248</height>\n" +
                    "<keepaspectratio>true</keepaspectratio>\n" +
                    "<texture>#StreamedMP.recentlyAdded.movie3.thumb</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 3 Rounded Cover</description>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1338</posX>\n" +
                    "<posY>705</posY>\n" +
                    "<height>272</height>\n" +
                    "<width>194</width>\n" +
                    "<texture>round.poster.frame.noreflection.png</texture>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 3 title</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>747</posY>\n" +
                    "<width>373</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie3.title</label>\n" +
                    "<font>mediastream12tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 3 runtime</description>\n" +
                    "<type>fadelabel</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>827</posY>\n" +
                    "<width>383</width>\n" +
                    "<label>#StreamedMP.recentlyAdded.movie3.runtime</label>\n" +
                    "<font>mediastream12</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                    "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Movie 3 certification</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>1530</posX>\n" +
                    "<posY>906</posY>\n" +
                    "<width>383</width>\n" +
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
        buildMovingPicturesSummaryFile(713, mostRecentMovPicsSummaryStyle.poster);
        writeXMLFile("basichome.recentlyadded.movpics.VSum.xml");
      }
      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.fanart)
      {
        buildMovingPicturesSummaryFile(713, mostRecentMovPicsSummaryStyle.fanart);
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
      int maxYpos = 804;
      int overlayOffset = 276;
      int hdOffset = 0;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + 81;
      int menuBot = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 45;

      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
        menuBot += (basicHomeValues.subMenuHeight - 45);

      if (menuBot > maxYpos)
      {
        overlayYpos = menuTop - overlayOffset;
        hdOffset -= 81;
      }
      else
        overlayYpos = 804;

      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        overlayYpos -= 120;

      // Build the file
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: RecentlyAdded Movie 1</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)</visible>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,450" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Movie 1 BG</description>" +
                    "<posX>42</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>578</width>" +
                    "<height>270</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>54</posX>" +
                    "<posY>" + (overlayYpos + 15).ToString() + "</posY>" +
                    "<height>245</height>" +
                    "<width>167</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.movie1.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>39</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<height>266</height>" +
                    "<width>190</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                "<control>" +
                    "<description>Movie 2 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>228</posX>" +
                    "<posY>" + (overlayYpos + 44).ToString() + "</posY>" +
                    "<width>387</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie1.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Runtime</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>228</posX>" +
                    "<posY>" + (overlayYpos + 123).ToString() + "</posY>" +
                    "<width>383</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie1.runtime</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 1 Certification</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>228</posX>" +
                    "<posY>" + (overlayYpos + 209).ToString() + "</posY>" +
                    "<width>383</width>" +
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
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,450" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Movie 2 BG</description>" +
                    "<posX>663</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>578</width>" +
                    "<height>270</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 2 thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>675</posX>" +
                    "<posY>" + (overlayYpos + 15).ToString() + "</posY>" +
                    "<height>245</height>" +
                    "<width>167</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.movie2.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>660</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<height>266</height>" +
                    "<width>190</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                  "<control>" +
                    "<description>Movie 2 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>852</posX>" +
                    "<posY>" + (overlayYpos + 44).ToString() + "</posY>" +
                    "<width>387</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie2.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 2 Runtime</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>848</posX>" +
                    "<posY>" + (overlayYpos + 123).ToString() + "</posY>" +
                    "<width>383</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 2 Certification</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>848</posX>" +
                    "<posY>" + (overlayYpos + 209).ToString() + "</posY>" +
                    "<width>387</width>" +
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
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,450" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                  "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                  "<control>" +
                    "<description>Movie 3 BG</description>" +
                    "<posX>1283</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<type>image</type>" +
                    "<id>6777</id>" +
                    "<width>578</width>" +
                    "<height>270</height>" +
                    "<texture>recentfullbg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1299</posX>" +
                    "<posY>" + (overlayYpos + 15).ToString() + "</posY>" +
                    "<height>245</height>" +
                    "<width>167</width>" +
                    "<keepaspectratio>true</keepaspectratio>" +
                    "<texture>#StreamedMP.recentlyAdded.movie3.thumb</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Rounded Cover</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1284</posX>" +
                    "<posY>" + (overlayYpos + 3).ToString() + "</posY>" +
                    "<height>266</height>" +
                    "<width>190</width>" +
                    "<texture>round.poster.frame.noreflection.png</texture>" +
                  "</control>      " +
                  "<control>" +
                    "<description>Movie 3 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>1472</posX>" +
                    "<posY>" + (overlayYpos + 44).ToString() + "</posY>" +
                    "<width>383</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie3.title</label>" +
                    "<font>mediastream12tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Runtime</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1472</posX>" +
                    "<posY>" + (overlayYpos + 123).ToString() + "</posY>" +
                    "<width>383</width>" +
                    "<label>#StreamedMP.recentlyAdded.movie3.runtime</label>" +
                    "<font>mediastream12</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Movie 3 Certification</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1472</posX>" +
                    "<posY>" + (overlayYpos + 209).ToString() + "</posY>" +
                    "<width>383</width>" +
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
      int overlayOffset = 336;
      int menuOffset = 75;
      if (menuStyle == chosenMenuStyle.horizontalContextStyle || horizontalContextLabels.Checked)
        menuOffset = 90;

      int menuTop = int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu + menuOffset;

      if (enableTwitter.Checked)
        menuTop = int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter + 45;

      if (menuStyle == chosenMenuStyle.graphicMenuStyle)
        menuTop -= 90;

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
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1292</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos).ToString() + "</posY>" +
                      "<width>203</width>" +
                      "<height>330</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 3 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1307</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos + 17).ToString() + "</posY>" +
                      "<width>173</width>" +
                      "<height>255</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.movie3.thumb</texture>" +
                    "</control>" +
                  "<control>" +
                    "<description>Movie 3 Star Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1310</posX>" +
                    "<posY>" + (sumStyle1OverlayYpos + 277).ToString() + "</posY>" +
                    "<width>164</width>" +
                    "<height>31</height>" +
                    "<texture>star#StreamedMP.recentlyAdded.movie3.score.png</texture>" +
                    "<align>left</align>" +
                  "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 2</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie2.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1502</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos).ToString() + "</posY>" +
                      "<width>203</width>" +
                      "<height>330</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Series 2 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1517</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos + 17).ToString() + "</posY>" +
                      "<width>173</width>" +
                      "<height>255</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.movie2.thumb</texture>" +
                    "</control>" +
                  "<control>" +
                    "<description>Movie 2 Star Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>1520</posX>" +
                    "<posY>" + (sumStyle1OverlayYpos + 277).ToString() + "</posY>" +
                    "<width>164</width>" +
                    "<height>31</height>" +
                    "<texture>star#StreamedMP.recentlyAdded.movie2.score.png</texture>" +
                    "<align>left</align>" +
                  "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>GROUP: RecentlyAdded Series 1</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+!string.starts(#StreamedMP.recentlyAdded.movie1.thumb,#)</visible>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,300" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>" +
                    "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>" +
                    "<control>" +
                      "<description>Movie 1 background</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1712</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos).ToString() + "</posY>" +
                      "<width>203</width>" +
                      "<height>330</height>" +
                      "<texture>pic_preview_thumb_background.png</texture>" +
                      "<colordiffuse>99FFFFFF</colordiffuse>" +
                    "</control>" +
                    "<control>" +
                      "<description>Movie 1 thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1727</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos + 17).ToString() + "</posY>" +
                      "<width>173</width>" +
                      "<height>255</height>" +
                      "<keepaspectratio>true</keepaspectratio>" +
                      "<texture>#StreamedMP.recentlyAdded.movie1.thumb</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Movie 1 Star rating</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>1730</posX>" +
                      "<posY>" + (sumStyle1OverlayYpos + 277).ToString() + "</posY>" +
                      "<width>164</width>" +
                      "<height>31</height>" +
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

        int xPos = baseXPosAdded + 30;

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
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                    "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
                    "<control>\n" +
                    "<description>Movie 1 BG</description>\n" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<type>image</type>\n" +
                    "<id>0</id>\n" +
                    "<width>459</width>\n" +
                    "<height>480</height>\n" +
                    "<texture>recentsummoverlaybg.png</texture>\n" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                  "</control>\n" +
                  "<control>\n" +
                    "<description>Header label</description>\n" +
                    "<type>label</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                    "<width>387</width>\n" +
                    "<label>#StreamedMP.LatestMovies</label>\n" +
                    "<font>mediastream10tc</font>\n" +
                    "<textcolor>White</textcolor>\n" +
                  "</control>      " +
                  "<control>\n" +
                    "<description>Movie 1 Title</description>\n" +
                    "<type>" + fadeLabelControl + "</type>\n" +
                    "<id>0</id>\n" +
                    "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 293).ToString() + "</posY>\n" +
                    "<width>387</width>\n" +
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
             "<posY>" + (baseYPosAdded + 318).ToString() + "</posY>\n" +
             "<width>386</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie1.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = 1770;
        }
        else
        {
          alignTxt = "left";
          xPos -= 8;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 1 Certification</description>\n" +
            "<type>fadelabel</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 318).ToString() + "</posY>\n" +
            "<width>150</width>\n" +
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
              "<posX>" + (baseXPosAdded + 426).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 323).ToString() + "</posY>\n" +
              "<width>105</width>" +
              "<height>20</height>" +
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
              "<posX>" + (baseXPosAdded + 321).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 323).ToString() + "</posY>\n" +
              "<width>105</width>" +
              "<height>20</height>" +
              "<texture>star#StreamedMP.recentlyAdded.movie1.score.png</texture>" +
            "</control>";
          }
        }

        xPos = baseXPosAdded + 30;
        xml += "<control>\n" +
                "<description>Movie 1 thumb/fanart</description>\n" +
                "<type>image</type>\n" +
                "<id>0</id>\n" +
                "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                "<posY>" + (baseYPosAdded + 63).ToString() + "</posY>\n" +
                "<width>402</width>\n" +
                "<height>227</height>\n" +
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
              "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
              "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
              "<control>\n" +
                "<description>Movie 2 Title</description>\n" +
                "<type>" + fadeLabelControl + "</type>\n" +
                "<id>0</id>\n" +
                "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                "<posY>" + (baseYPosAdded + 345).ToString() + "</posY>\n" +
                "<width>387</width>\n" +
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
            "<posY>" + (baseYPosAdded + 371).ToString() + "</posY>\n" +
            "<width>386</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie2.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = baseXPosAdded + 306;
        }
        else
        {
          xPos -= 8;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 2 Certification</description>\n" +
            "<type>fadelabel</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 371).ToString() + "</posY>\n" +
            "<width>150</width>\n" +
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
              "<posX>" + (baseXPosAdded + 426).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 371).ToString() + "</posY>\n" +
              "<width>105</width>" +
              "<height>20</height>" +
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
              "<posX>" + (baseXPosAdded + 321).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 377).ToString() + "</posY>\n" +
              "<width>105</width>" +
              "<height>20</height>" +
              "<texture>star#StreamedMP.recentlyAdded.movie2.score.png</texture>" +
            "</control>";
          }
        }

        xPos = baseXPosAdded + 30;
        xml += "</control>\n" +
        "<control>\n" +
          "<description>GROUP: RecentlyAdded Movie 3</description>\n" +
          "<type>group</type>\n" +
          "<dimColor>0xffffffff</dimColor>\n" +
          "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "+string.equals(#StreamedMP.recentlyAdded.movie3.show,true)</visible>" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
          "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
          "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n" +
          "<control>\n" +
            "<type>" + fadeLabelControl + "</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 398).ToString() + "</posY>\n" +
            "<width>387</width>\n" +
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
            "<posY>" + (baseYPosAdded + 423).ToString() + "</posY>\n" +
            "<width>386</width>\n" +
            "<label>#StreamedMP.recentlyAdded.movie3.runtime</label>\n" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<textcolor>White</textcolor>\n" +
          "</control>";
          xPos = baseXPosAdded + 306;
        }
        else
        {
          xPos -= 8;
        }

        if (!movPicsOptions.HideCertification)
        {
          xml += "<control>\n" +
            "<description>Movie 3 Certification</description>\n" +
            "<type>fadelabel</type>\n" +
            "<id>0</id>\n" +
            "<posX>" + xPos.ToString() + "</posX>\n" +
            "<posY>" + (baseYPosAdded + 423).ToString() + "</posY>\n" +
            "<width>150</width>\n" +
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
              "<posX>" + (baseXPosAdded + 426).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 423).ToString() + "</posY>\n" +
              "<width>105</width>" +
              "<height>20</height>" +
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
              "<posX>" + (baseXPosAdded + 321).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosAdded + 428).ToString() + "</posY>\n" +
              "<width>105</width>" +
              "<height>20</height>" +
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
      int xPos = baseXPosWatched + 30;

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
      if (baseXPosWatched > 960)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
                "<description>Movie 1 BG</description>\n" +
                  "<posX>" + baseXPosWatched.ToString() + "</posX>\n" +
                  "<posY>" + baseYPosWatched.ToString() + "</posY>\n" +
                  "<type>image</type>\n" +
                  "<id>0</id>\n" +
                  "<width>459</width>\n" +
                  "<height>480</height>\n" +
                  "<texture>recentsummoverlaybg.png</texture>\n" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>Header label</description>\n" +
                  "<type>label</type>\n" +
                  "<id>0</id>\n" +
                  "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosWatched + 30).ToString() + "</posY>\n" +
                  "<width>387</width>\n" +
                  "<label>#StreamedMP.RecentlyWatched</label>\n" +
                  "<font>mediastream10tc</font>\n" +
                  "<textcolor>White</textcolor>\n" +
                "</control>      " +
                "<control>\n" +
                  "<description>Movie 1 Title</description>\n" +
                  "<type>" + fadelabelControl + "</type>\n" +
                  "<id>0</id>\n" +
                  "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosWatched + 293).ToString() + "</posY>\n" +
                  "<width>387</width>\n" +
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
          "<posY>" + (baseYPosWatched + 318).ToString() + "</posY>\n" +
          "<width>386</width>\n" +
          "<label>#StreamedMP.recentlyWatched.movie1.runtime</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
        "</control>";
        xPos = baseXPosWatched + 306;
      }
      else
      {
        alignTxt = "left";
        xPos -= 8;
      }

      if (!movPicsOptions.HideCertification)
      {
        xml += "<control>\n" +
          "<description>Movie 1 Certification</description>\n" +
          "<type>fadelabel</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 318).ToString() + "</posY>\n" +
          "<width>150</width>\n" +
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
            "<posX>" + (baseXPosWatched + 426).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 323).ToString() + "</posY>\n" +
            "<width>105</width>" +
            "<height>20</height>" +
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
            "<posX>" + (baseXPosWatched + 321).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 323).ToString() + "</posY>\n" +
            "<width>105</width>" +
            "<height>20</height>" +
            "<texture>star#StreamedMP.recentlyWatched.movie1.score.png</texture>" +
          "</control>";
        }
      }

      xPos = baseXPosWatched + 29;
      xml += "<control>\n" +
              "<description>Movie 1 thumb/fanart</description>\n" +
              "<type>image</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 63).ToString() + "</posY>\n" +
              "<width>402</width>\n" +
              "<height>227</height>\n" +
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
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
            "<description>Movie 2 Title</description>\n" +
              "<type>" + fadelabelControl + "</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 345).ToString() + "</posY>\n" +
              "<width>387</width>\n" +
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
          "<posY>" + (baseYPosWatched + 371).ToString() + "</posY>\n" +
          "<width>386</width>\n" +
          "<label>#StreamedMP.recentlyWatched.movie2.runtime</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
        "</control>";
        xPos = baseXPosWatched + 306;
      }
      else
      {
        xPos -= 8;
      }

      if (!movPicsOptions.HideCertification)
      {
        xml += "<control>\n" +
          "<description>Movie 2 Certification</description>\n" +
          "<type>fadelabel</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 371).ToString() + "</posY>\n" +
          "<width>150</width>\n" +
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
            "<posX>" + (baseXPosWatched + 426).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 371).ToString() + "</posY>\n" +
            "<width>105</width>" +
            "<height>20</height>" +
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
            "<posX>" + (baseXPosWatched + 321).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 377).ToString() + "</posY>\n" +
            "<width>105</width>" +
            "<height>20</height>" +
            "<texture>star#StreamedMP.recentlyWatched.movie2.score.png</texture>" +
          "</control>";
        }
      }

      xPos = baseXPosWatched + 29;
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
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
          "<type>" + fadelabelControl + "</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 398).ToString() + "</posY>\n" +
          "<width>387</width>\n" +
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
          "<posY>" + (baseYPosWatched + 423).ToString() + "</posY>\n" +
          "<width>386</width>\n" +
          "<label>#StreamedMP.recentlyWatched.movie3.runtime</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
        "</control>";
        xPos = baseXPosWatched + 306;
      }
      else
      {
        xPos -= 8;
      }

      if (!movPicsOptions.HideCertification)
      {
        xml += "<control>\n" +
          "<description>Movie 3 Certification</description>\n" +
          "<type>fadelabel</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 423).ToString() + "</posY>\n" +
          "<width>150</width>\n" +
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
            "<posX>" + (baseXPosWatched + 426).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 423).ToString() + "</posY>\n" +
            "<width>105</width>" +
            "<height>20</height>" +
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
            "<posX>" + (baseXPosWatched + 321).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 428).ToString() + "</posY>\n" +
            "<width>105</width>" +
            "<height>20</height>" +
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
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
              "<description>Series 1 BG</description>\n" +
              "<posX>" + baseXPosWatched.ToString() + "</posX>\n" +
              "<posY>" + baseYPosWatched.ToString() + "</posY>\n" +
              "<type>image</type>\n" +
              "<id>0</id>\n" +
              "<width>459</width>\n" +
              "<height>480</height>\n" +
              "<texture>recentsummoverlaybg.png</texture>\n" +
              "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
            "</control>\n" +
            "<control>\n" +
              "<description>Header label</description>\n" +
              "<type>label</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 30).ToString() + "</posY>\n" +
              "<width>387</width>\n" +
              "<label>#StreamedMP.RecentlyWatched</label>\n" +
              "<font>mediastream10tc</font>\n" +
              "<textcolor>White</textcolor>\n" +
            "</control>      " +
            "<control>\n" +
              "<description>Series 1 name</description>\n" +
              "<type>" + fadeLabelControl + "</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 293).ToString() + "</posY>\n" +
              "<width>387</width>\n" +
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
      "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 318).ToString() + "</posY>\n" +
      "<width>383</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>" +
      "<label>#StreamedMP.recentlyWatched.series1.episodetitle</label>\n" +
      "<font>" + mrEpisodeFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>" +
    "<control>\n" +
      "<description>Series 1 thumb/fanart</description>\n" +
      "<type>image</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 63).ToString() + "</posY>\n" +
      "<width>402</width>\n" +
      "<height>227</height>\n" +
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
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
      "<description>Series 2 name</description>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 345).ToString() + "</posY>\n" +
      "<width>387</width>\n" +
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
      "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 371).ToString() + "</posY>\n" +
      "<width>383</width>\n" +
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
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-450,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-450,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-600,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-600,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + "400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 398).ToString() + "</posY>\n" +
      "<width>387</width>\n" +
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
      "<posX>" + (baseXPosWatched + 29).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 423).ToString() + "</posY>\n" +
      "<width>383</width>\n" +
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
                "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Music 1 BG</description>" +
                  "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                  "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<width>459</width>" +
                  "<height>480</height>" +
                  "<texture>recentsummoverlaybg.png</texture>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>" +
                "</control>" +
                "<control>" +
                  "<description>Header label</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                  "<width>387</width>" +
                  "<label>#StreamedMP.LatestMusic</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Music 1 Artist</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 293).ToString() + "</posY>\n" +
                  "<label>" + latestMediaPrefix + ".music.latest1.artist</label>" +
                  "<textcolor>White</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>Music 1 Album</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 318).ToString() + "</posY>\n" +
                  "<width>386</width>" +
                  "<label>" + latestMediaPrefix + ".music.latest1.album</label>" +
                  "<font>mediastream10c</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Music1 thumb/fanart</description>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 107).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 60).ToString() + "</posY>\n" +
                  "<width>230</width>" +
                  "<height>230</height>" +
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
                "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Music 2 Artist</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 345).ToString() + "</posY>\n" +
                  "<width>387</width>" +
                  "<label>" + latestMediaPrefix + ".music.latest2.artist</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>Movie 2 Album</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 371).ToString() + "</posY>\n" +
                  "<width>386</width>" +
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
                "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Music 3 Artist</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 398).ToString() + "</posY>\n" +
                  "<width>387</width>" +
                  "<label>" + latestMediaPrefix + ".music.latest3.artist</label>" +
                  "<textcolor>White</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>Music 3 Album</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 423).ToString() + "</posY>\n" +
                  "<width>386</width>" +
                  "<label>" + latestMediaPrefix + ".music.latest3.album</label>" +
                  "<font>mediastream10c</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
              "</control>" +
            "</controls>" +
          "</window>";
    }

    #endregion

    #region Most Recent Music Videos

    void MostRecentMusicVideosSummary()
    {
      xml = "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>\n" +
            "<window>\n" +
              "<controls>\n" +
                "<control>" +
                "<description>GROUP: RecentlyAdded Music Videos1</description>" +
                "<type>group</type>" +
                "<dimColor>0xffffffff</dimColor>";

      xml += "<visible>" + mostRecentVisibleControls(isOverlayType.MusicVideos) + "+[string.equals(#mvCentral.latest.enabled,true)]" + "</visible>";

      xml += "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
             "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
             "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
             "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
             "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
             "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
             "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
             "<control>" +
               "<description>Music 1 BG</description>" +
               "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
               "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
               "<type>image</type>" +
               "<id>0</id>" +
               "<width>459</width>" +
               "<height>480</height>" +
               "<texture>recentsummoverlaybg.png</texture>" +
               "<colordiffuse>EEFFFFFF</colordiffuse>" +
             "</control>" +
             "<control>" +
               "<description>Header label</description>" +
               "<type>label</type>" +
               "<id>0</id>" +
               "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
               "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
               "<width>387</width>" +
               "<label>#StreamedMP.LatestMusicVideos</label>" +
               "<font>mediastream10tc</font>" +
               "<textcolor>White</textcolor>" +
             "</control>" +
             "<control>" +
               "<description>Music Video 1 Artist</description>" +
               "<type>label</type>" +
               "<id>0</id>" +
               "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
               "<posY>" + (baseYPosAdded + 293).ToString() + "</posY>\n" +
               "<label>#mvCentral.Latest.Artist1</label>" +
               "<textcolor>White</textcolor>" +
               "<font>mediastream10tc</font>" +
               "<scrollStartDelaySec>20</scrollStartDelaySec>" +
             "</control>" +
             "<control>" +
               "<description>Music 1 Track</description>" +
               "<type>label</type>" +
               "<id>0</id>" +
               "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
               "<posY>" + (baseYPosAdded + 318).ToString() + "</posY>\n" +
               "<width>386</width>" +
               "<label>#mvCentral.Latest.Track1</label>" +
               "<font>mediastream10c</font>" +
               "<textcolor>White</textcolor>" +
             "</control>" +
             "<control>" +
               "<description>Music Video 1 thumb/fanart</description>" +
               "<type>image</type>" +
               "<id>0</id>" +
               "<posX>" + (baseXPosAdded + 107).ToString() + "</posX>\n" +
               "<posY>" + (baseYPosAdded + 63).ToString() + "</posY>\n" +
               "<width>233</width>" +
               "<height>233</height>" +
               "<keepaspectratio>true</keepaspectratio>" +
               "<texture>#mvCentral.Latest.ArtistImage1</texture>" +
               "<shouldCache>true</shouldCache>\n" +
             "</control>" +
           "</control>" +
           "<control>" +
             "<description>GROUP: RecentlyAdded Music Video 2</description>" +
             "<type>group</type>" +
             "<dimColor>0xffffffff</dimColor>";

      xml += "<visible>" + mostRecentVisibleControls(isOverlayType.MusicVideos) + "+[string.equals(#mvCentral.latest.enabled,true)]" + "</visible>";

      xml += "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
             "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
             "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
             "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
             "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
             "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
             "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
             "<control>" +
               "<description>Music 2 Video Artist</description>" +
               "<type>label</type>" +
               "<id>0</id>" +
               "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
               "<posY>" + (baseYPosAdded + 345).ToString() + "</posY>\n" +
               "<width>387</width>" +
               "<label>#mvCentral.Latest.Artist2</label>" +
               "<font>mediastream10tc</font>" +
               "<textcolor>White</textcolor>" +
               "<scrollStartDelaySec>20</scrollStartDelaySec>" +
             "</control>" +
             "<control>" +
               "<description>Music Video 2 Track</description>" +
               "<type>label</type>" +
               "<id>0</id>" +
               "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
               "<posY>" + (baseYPosAdded + 371).ToString() + "</posY>\n" +
               "<width>386</width>" +
               "<label>#mvCentral.Latest.Track2</label>" +
               "<font>mediastream10c</font>" +
               "<textcolor>White</textcolor>" +
             "</control>" +
           "</control>" +
           "<control>" +
             "<description>GROUP: RecentlyAdded Music Video 3</description>" +
             "<type>group</type>" +
             "<dimColor>0xffffffff</dimColor>";

      xml += "<visible>" + mostRecentVisibleControls(isOverlayType.MusicVideos) + "+[string.equals(#mvCentral.latest.enabled,true)]" + "</visible>";

      xml += "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
             "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
             "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
             "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
             "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
             "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
             "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
             "<control>" +
               "<description>Music 3 Artist</description>" +
               "<type>label</type>" +
               "<id>0</id>" +
               "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
               "<posY>" + (baseYPosAdded + 398).ToString() + "</posY>\n" +
               "<width>387</width>" +
               "<label>#mvCentral.Latest.Artist3</label>" +
               "<textcolor>White</textcolor>" +
               "<font>mediastream10tc</font>" +
               "<scrollStartDelaySec>20</scrollStartDelaySec>" +
             "</control>" +
             "<control>" +
               "<description>Music Video 3 Track</description>" +
               "<type>label</type>" +
               "<id>0</id>" +
               "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
               "<posY>" + (baseYPosAdded + 423).ToString() + "</posY>\n" +
               "<width>386</width>" +
               "<label>#mvCentral.Latest.Track3</label>" +
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
                "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>RecordedTV 1 BG</description>" +
                  "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                  "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<width>459</width>" +
                  "<height>480</height>" +
                  "<texture>recentsummoverlaybg.png</texture>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV Header label</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                  "<width>387</width>" +
                  "<label>#StreamedMP.LatestTVRecordings</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV 1 title</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>1493</posX>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 293).ToString() + "</posY>\n" +
                  "<label>" + latestMediaPrefix + ".tvrecordings.latest1.title</label>" +
                  "<textcolor>White</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV 1 dateAdded</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 318).ToString() + "</posY>\n" +
                  "<width>386</width>" +
                  "<label>#StreamedMP.RecordedOn " + latestMediaPrefix + ".tvrecordings.latest1.dateAdded</label>" +
                  "<font>mediastream10c</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV thumb/fanart</description>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 63).ToString() + "</posY>\n" +
                  "<width>402</width>" +
                  "<height>227</height>" +
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
                "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>RecordedTV 2 title</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 345).ToString() + "</posY>\n" +
                  "<width>387</width>" +
                  "<label>" + latestMediaPrefix + ".tvrecordings.latest2.title</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV 2 dateAdded</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 371).ToString() + "</posY>\n" +
                  "<width>386</width>" +
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
                "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>RecordedTV 3 title</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 398).ToString() + "</posY>\n" +
                  "<width>387</width>" +
                  "<label>" + latestMediaPrefix + ".tvrecordings.latest3.title</label>" +
                  "<textcolor>White</textcolor>" +
                  "<font>mediastream10tc</font>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>" +
                "<control>" +
                  "<description>RecordedTV 3 dateAdded</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>" + (baseXPosAdded + 29).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosAdded + 423).ToString() + "</posY>\n" +
                  "<width>386</width>" +
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
                          "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                          "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                          "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                          "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                          "<control>" +
                          "<description>Overlay BG</description>" +
                          "<posX>1464</posX>" +
                          "<posY>75</posY>" +
                          "<type>image</type>" +
                          "<id>0</id>" +
                          "<width>459</width>" +
                          "<height>480</height>" +
                          "<texture>recentsummoverlaybg.png</texture>" +
                          "<colordiffuse>EEFFFFFF</colordiffuse>" +
                          "</control>" +
                          "<control>" +
                          "<description>Plugin Name</description>" +
                          "<type>label</type>" +
                          "<id>0</id>" +
                          "<posX>1493</posX>" +
                          "<posY>114</posY>" +
                          "<width>387</width>" +
                          "<label>Drive Free Space</label>" +
                          "<font>mediastream10tc</font>" +
                          "<textcolor>White</textcolor>" +
                          "</control>" +
                          "<control>" +
                          "<description>Index Separator</description>" +
                          "<type>label</type>" +
                          "<id>0</id>" +
                          "<posX>1493</posX>" +
                          "<posY>120</posY>" +
                          "<width>396</width>" +
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
                    "<posX>1493</posX>" +
                    "<posY>" + (135 + yOffset).ToString() + "</posY>" +
                    "<width>90</width>" +
                    "<height>90</height>" +
                    "<texture>" + driveIcon + "</texture>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>Drive " + driveLetter + " Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>1493</posX>" +
                    "<posY>" + (240 + yOffset).ToString() + "</posY>" +
                    "<width>399</width>" +
                    "<height>30</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>Drive " + driveLetter + " Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>1478</posX>" +
                    "<posY>" + (242 + yOffset).ToString() + "</posY>" +
                    "<width>399</width>" +
                    "<height>30</height>" +
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
                    "<posX>1590</posX>" +
                    "<posY>" + (150 + yOffset).ToString() + "</posY>" +
                    "<width>297</width>" +
                    "<label>#DriveFreeSpace." + driveLetter + ".AvailableSpace.Data</label>" +
                    "<font>mediastream10</font>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>";

        yOffset += 126;
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
                  "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Overlay BG</description>" +
                  "<posX>1464</posX>" +
                  "<posY>75</posY>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<width>459</width>" +
                  "<height>480</height>" +
                  "<texture>recentsummoverlaybg.png</texture>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>" +
                "</control>" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>1493</posX>" +
                  "<posY>114</posY>" +
                  "<width>387</width>" +
                  "<label>Sleep Control</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>1493</posX>" +
                  "<posY>120</posY>" +
                  "<width>396</width>" +
                  "<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Mode Image</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1710</posX>" +
                  "<posY>111</posY>" +
                  "<width>30</width>" +
                  "<height>30</height>" +
                  "<texture>#SleepControl.Image</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Counter</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1464</posX>" +
                  "<posY>225</posY>" +
                  "<width>459</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Counter</label>" +
                  "<font>mediastream28tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Activity</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1464</posX>" +
                  "<posY>300</posY>" +
                  "<width>459</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Activity</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Mode</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1464</posX>" +
                  "<posY>345</posY>" +
                  "<width>459</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Method</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Start</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1464</posX>" +
                  "<posY>390</posY>" +
                  "<width>459</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Start</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control End</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1464</posX>" +
                  "<posY>435</posY>" +
                  "<width>459</width>" +
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
                  "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Overlay BG</description>" +
                  "<posX>1464</posX>" +
                  "<posY>75</posY>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<width>459</width>" +
                  "<height>480</height>" +
                  "<texture>recentsummoverlaybg.png</texture>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>" +
                "</control>" +
                "<!-- *** Indices *** -->" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>1493</posX>" +
                  "<posY>114</posY>" +
                  "<width>387</width>" +
                  "<label>Indices</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>1493</posX>" +
                  "<posY>120</posY>" +
                  "<width>396</width>" +
                  "<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<!-- *** Index 0 *** -->" +
                "<control>" +
                  "<description>Stocks.Index0Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1493</posX>" +
                  "<posY>150</posY>" +
                  "<width>27</width>" +
                  "<height>27</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Index0Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index0Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>150</width>" +
                  "<posX>1521</posX>" +
                  "<posY>149</posY>" +
                  "<label>#Stocks.Index0Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index0Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1785</posX>" +
                  "<posY>149</posY>" +
                  "<label>#Stocks.Index0Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index0PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1890</posX>" +
                  "<posY>149</posY>" +
                  "<label>#Stocks.Index0PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Index 1 *** -->" +
                "<control>" +
                  "<description>Stocks.Index1Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1493</posX>" +
                  "<posY>180</posY>" +
                  "<width>27</width>" +
                  "<height>27</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Index1Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index1Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>150</width>" +
                  "<posX>1521</posX>" +
                  "<posY>179</posY>" +
                  "<label>#Stocks.Index1Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index1Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1785</posX>" +
                  "<posY>179</posY>" +
                  "<label>#Stocks.Index1Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index1PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1890</posX>" +
                  "<posY>179</posY>" +
                  "<label>#Stocks.Index1PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Index 2 *** -->" +
                "<control>" +
                  "<description>Stocks.Index2Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1493</posX>" +
                  "<posY>210</posY>" +
                  "<width>27</width>" +
                  "<height>27</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Index2Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index2Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>150</width>" +
                  "<posX>1521</posX>" +
                  "<posY>209</posY>" +
                  "<label>#Stocks.Index2Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index2Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1785</posX>" +
                  "<posY>209</posY>" +
                  "<label>#Stocks.Index2Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index2PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1890</posX>" +
                  "<posY>209</posY>" +
                  "<label>#Stocks.Index2PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Stocks *** -->" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>1493</posX>" +
                  "<posY>248</posY>" +
                  "<width>395</width>" +
                  "<label>Stocks</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>1493</posX>" +
                  "<posY>254</posY>" +
                  "<width>396</width>" +
                  "<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<!-- *** Stock 0 *** -->" +
                "<control>" +
                  "<description>Stocks.Stock0Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1493</posX>" +
                  "<posY>284</posY>" +
                  "<width>27</width>" +
                  "<height>27</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Stock0Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock0Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>150</width>" +
                  "<posX>1521</posX>" +
                  "<posY>282</posY>" +
                  "<label>#Stocks.Stock0Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock0Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1785</posX>" +
                  "<posY>282</posY>" +
                  "<label>#Stocks.Stock0Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock0PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1890</posX>" +
                  "<posY>282</posY>" +
                  "<label>#Stocks.Stock0PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Stock 1 *** -->" +
                "<control>" +
                  "<description>Stocks.Stock1Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1493</posX>" +
                  "<posY>314</posY>" +
                  "<width>27</width>" +
                  "<height>27</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Stock1Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock1Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>150</width>" +
                  "<posX>1521</posX>" +
                  "<posY>314</posY>" +
                  "<label>#Stocks.Stock1Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock1Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1785</posX>" +
                  "<posY>314</posY>" +
                  "<label>#Stocks.Stock1Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock1PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1890</posX>" +
                  "<posY>314</posY>" +
                  "<label>#Stocks.Stock1PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Stock 2 *** -->" +
                "<control>" +
                  "<description>Stocks.Stock2Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1493</posX>" +
                  "<posY>344</posY>" +
                  "<width>27</width>" +
                  "<height>27</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Stock2Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock2Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>150</width>" +
                  "<posX>1521</posX>" +
                  "<posY>342</posY>" +
                  "<label>#Stocks.Stock2Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock2Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1785</posX>" +
                  "<posY>342</posY>" +
                  "<label>#Stocks.Stock2Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                    "<description>Stocks.Stock2PercentChange</description>" +
                    "<type>label</type>" +
                    "<id>1</id>" +
                    "<posX>1890</posX>" +
                    "<posY>342</posY>" +
                    "<label>#Stocks.Stock2PercentChange</label>" +
                    "<font>mediastream10c</font>" +
                    "<align>right</align>" +
                "</control>" +
                "<!-- *** Currencies *** -->" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>1493</posX>" +
                  "<posY>381</posY>" +
                  "<width>387</width>" +
                  "<label>Currencies</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>1493</posX>" +
                  "<posY>387</posY>" +
                  "<width>396</width>" +
                  "<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<!-- *** Currency 0 *** -->" +
                "<control>" +
                  "<description>Stocks.Currency0Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1493</posX>" +
                  "<posY>417</posY>" +
                  "<width>27</width>" +
                  "<height>27</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Currency0Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency0Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>285</width>" +
                  "<posX>1521</posX>" +
                  "<posY>416</posY>" +
                  "<label>#Stocks.Currency0Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency0Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1890</posX>" +
                  "<posY>416</posY>" +
                  "<label>#Stocks.Currency0Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Currency 1 *** -->" +
                "<control>" +
                  "<description>Stocks.Currency1Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1493</posX>" +
                  "<posY>447</posY>" +
                  "<width>27</width>" +
                  "<height>27</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Currency1Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency1Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>285</width>" +
                  "<posX>1521</posX>" +
                  "<posY>446</posY>" +
                  "<label>#Stocks.Currency1Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency1Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1890</posX>" +
                  "<posY>446</posY>" +
                  "<label>#Stocks.Currency1Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Currency 2 *** -->" +
                "<control>" +
                  "<description>Stocks.Currency2Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1493</posX>" +
                  "<posY>477</posY>" +
                  "<width>27</width>" +
                  "<height>27</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Currency2Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency2Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>285</width>" +
                  "<posX>1521</posX>" +
                  "<posY>476</posY>" +
                  "<label>#Stocks.Currency2Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency2Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1890</posX>" +
                  "<posY>476</posY>" +
                  "<label>#Stocks.Currency2Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Refresh Time *** -->" +
                "<control>" +
                  "<description>Stocks.Time</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1890</posX>" +
                  "<posY>114</posY>" +
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
                  "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                  "<control>" +
                    "<description>Movie 1 BG</description>" +
                    "<posX>1464</posX>" +
                    "<posY>75</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>459</width>" +
                    "<height>480</height>" +
                    "<texture>recentsummoverlaybg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Header label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1493</posX>" +
                    "<posY>114</posY>" +
                    "<width>387</width>" +
                    "<label>Power Control</label>" +
                    "<font>mediastream10tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Index Separator</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1493</posX>" +
                    "<posY>120</posY>" +
                    "<width>396</width>" +
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
                      "<posX>1493</posX>" +
                      "<posY>150</posY>" +
                      "<width>60</width>" +
                      "<height>60</height>" +
                      "<texture>#PowerControl.NetworkDevice0TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 0 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1563</posX>" +
                      "<posY>165</posY>" +
                      "<width>297</width>" +
                      "<label>#PowerControl.NetworkDevice0Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 0 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1860</posX>" +
                      "<posY>165</posY>>" +
                      "<texture>#PowerControl.NetworkDevice0AliveImage</texture>" +
                      "<width>30</width>" +
                      "<height>30</height>" +
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
                      "<posX>1493</posX>" +
                      "<posY>228</posY>" +
                      "<width>60</width>" +
                      "<height>60</height>" +
                      "<texture>#PowerControl.NetworkDevice1TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 1 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1563</posX>" +
                      "<posY>243</posY>" +
                      "<label>#PowerControl.NetworkDevice1Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 1 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1860</posX>" +
                      "<posY>243</posY>>" +
                      "<texture>#PowerControl.NetworkDevice1AliveImage</texture>" +
                      "<width>30</width>" +
                      "<height>30</height>" +
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
                      "<posX>1493</posX>" +
                      "<posY>306</posY>" +
                      "<width>60</width>" +
                      "<height>60</height>" +
                      "<texture>#PowerControl.NetworkDevice2TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                        "<description>Network Device 2 Description</description>" +
                        "<type>label</type>" +
                        "<id>1</id>" +
                        "<posX>1563</posX>" +
                        "<posY>321</posY>" +
                        "<label>#PowerControl.NetworkDevice2Description</label>" +
                        "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 2 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1860</posX>" +
                      "<posY>321</posY>>" +
                      "<texture>#PowerControl.NetworkDevice2AliveImage</texture>" +
                      "<width>30</width>" +
                      "<height>30</height>" +
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
                      "<posX>1493</posX>" +
                      "<posY>384</posY>" +
                      "<width>60</width>" +
                      "<height>60</height>" +
                      "<texture>#PowerControl.NetworkDevice3TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 3 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1563</posX>" +
                      "<posY>399</posY>" +
                      "<label>#PowerControl.NetworkDevice3Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 3 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1860</posX>" +
                      "<posY>399</posY>>" +
                      "<texture>#PowerControl.NetworkDevice3AliveImage</texture>" +
                      "<width>30</width>" +
                      "<height>30</height>" +
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
                      "<posX>1493</posX>" +
                      "<posY>462</posY>" +
                      "<width>60</width>" +
                      "<height>60</height>" +
                      "<texture>#PowerControl.NetworkDevice4TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 4 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1563</posX>" +
                      "<posY>477</posY>" +
                      "<label>#PowerControl.NetworkDevice4Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 4 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1860</posX>" +
                      "<posY>477</posY>" +
                      "<texture>#PowerControl.NetworkDevice4AliveImage</texture>" +
                      "<width>30</width>" +
                      "<height>30</height>" +
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
                  "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                  "<control>" +
                    "<description>Overlay BG</description>" +
                    "<posX>1464</posX>" +
                    "<posY>75</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>459</width>" +
                    "<height>480</height>" +
                    "<texture>recentsummoverlaybg.png</texture>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Plugin Name</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1493</posX>" +
                    "<posY>114</posY>" +
                    "<width>387</width>" +
                    "<label>HTPC Info</label>" +
                    "<font>mediastream10tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Index Separator</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1493</posX>" +
                    "<posY>120</posY>" +
                    "<width>396</width>" +
                    "<label>____________________________________________________________________________________________________________</label>" +
                    "<textcolor>ff808080</textcolor>" +
                  "</control>" +
                  "<!-- *** CPU Infos *** -->" +
                  "<control>" +
                    "<description>CPU Picture</description>" +
                    "<posX>1493</posX>" +
                    "<posY>150</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>120</width>" +
                    "<height>120</height>" +
                    "<texture>HTPC_Info_CPU.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Temp label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1635</posX>" +
                    "<posY>173</posY>" +
                    "<width>255</width>" +
                    "<label>CPU Temp:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Temp value</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1883</posX>" +
                    "<posY>173</posY>" +
                    "<width>375</width>" +
                    "<label>#HTPCInfo.SensorTemperatureCPU</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.SensorTemperatureCPU,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Usage label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1635</posX>" +
                    "<posY>203</posY>" +
                    "<width>255</width>" +
                    "<label>CPU Usage:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Processor Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1883</posX>" +
                    "<posY>203</posY>" +
                    "<width>375</width>" +
                    "<label>#HTPCInfo.ProcessorUsage%</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.ProcessorUsage,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>1629</posX>" +
                    "<posY>231</posY>" +
                    "<width>264</width>" +
                    "<height>30</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>1611</posX>" +
                    "<posY>233</posY>" +
                    "<width>294</width>" +
                    "<height>30</height>" +
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
                    "<posX>1493</posX>" +
                    "<posY>278</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>120</width>" +
                    "<height>120</height>" +
                    "<texture>HTPC_Info_RAM.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>Free RAM label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1635</posX>" +
                    "<posY>300</posY>" +
                    "<width>255</width>" +
                    "<label>Free RAM:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Free RAM Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1883</posX>" +
                    "<posY>300</posY>" +
                    "<width>375</width>" +
                    "<label>#HTPCInfo.MemoryAvailable</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.MemoryAvailable,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>RAM Usage label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1635</posX>" +
                    "<posY>330</posY>" +
                    "<width>255</width>" +
                    "<label>RAM Usage:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Ram Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1883</posX>" +
                    "<posY>330</posY>" +
                    "<width>375</width>" +
                    "<label>#HTPCInfo.MemoryPercentUsed%</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.MemoryPercentUsed,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>RAM Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>1629</posX>" +
                    "<posY>359</posY>" +
                    "<width>264</width>" +
                    "<height>30</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>RAM Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>1611</posX>" +
                    "<posY>360</posY>" +
                    "<width>294</width>" +
                    "<height>30</height>" +
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
                    "<posX>1493</posX>" +
                    "<posY>413</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>120</width>" +
                    "<height>120</height>" +
                    "<texture>HTPC_Info_GPU.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Temp label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1635</posX>" +
                    "<posY>435</posY>" +
                    "<width>255</width>" +
                    "<label>GPU Temp:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Temp value</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1883</posX>" +
                    "<posY>435</posY>" +
                    "<width>375</width>" +
                    "<label>#HTPCInfo.GPUSensorTemperature0</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.GPUSensorTemperature0,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Usage label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1635</posX>" +
                    "<posY>465</posY>" +
                    "<width>255</width>" +
                    "<label>GPU Usage:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1883</posX>" +
                    "<posY>465</posY>" +
                    "<width>375</width>" +
                    "<label>#HTPCInfo.GPUSensorUsage0%</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.GPUSensorUsage0,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>1629</posX>" +
                    "<posY>494</posY>" +
                    "<width>264</width>" +
                    "<height>30</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>1611</posX>" +
                    "<posY>495</posY>" +
                    "<width>294</width>" +
                    "<height>30</height>" +
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
                          "<animation effect=\"slide\" end=\"450,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                          "<animation effect=\"slide\" start=\"450,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                          "<animation effect=\"slide\" start=\"600,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                          "<animation effect=\"slide\" end=\"600,0\" tween=\"quadratic\" easing=\"in\" time=\"400\" delay=\"200\">WindowClose</animation>" +
                          "<control>" +
                              "<description>Overlay BG</description>" +
                              "<posX>1464</posX>" +
                              "<posY>75</posY>" +
                              "<type>image</type>" +
                              "<id>0</id>" +
                              "<width>459</width>" +
                              "<height>480</height>" +
                              "<texture>recentsummoverlaybg.png</texture>" +
                              "<colordiffuse>EEFFFFFF</colordiffuse>" +
                          "</control>" +
                          "<control>" +
                              "<description>Plugin Name</description>" +
                              "<type>label</type>" +
                              "<id>0</id>" +
                              "<posX>1493</posX>" +
                              "<posY>114</posY>" +
                              "<width>387</width>" +
                              "<label>Update Control</label>" +
                              "<font>mediastream10tc</font>" +
                              "<textcolor>White</textcolor>" +
                          "</control>" +
                          "<control>" +
                              "<description>Index Separator</description>" +
                              "<type>label</type>" +
                              "<id>0</id>" +
                              "<posX>1493</posX>" +
                              "<posY>120</posY>" +
                              "<width>396</width>" +
                              "<label>____________________________________________________________________________________________________________</label>" +
                              "<textcolor>ff808080</textcolor>" +
                          "</control>" +
                          "<control>" +
                              "<description>Urgency Image</description>" +
                              "<type>image</type>" +
                              "<id>1</id>" +
                              "<posX>1493</posX>" +
                              "<posY>173</posY>" +
                              "<width>90</width>" +
                              "<height>90</height>" +
                              "<texture>updatecontrol_empty.png</texture>" +
                              "<visible>string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>" +
                          "<control>" +
                              "<description>Urgency Image</description>" +
                              "<type>image</type>" +
                              "<id>1</id>" +
                              "<posX>1493</posX>" +
                              "<posY>173</posY>" +
                              "<width>90</width>" +
                              "<height>90</height>" +
                              "<texture>#UpdateControl.AvailableUpdateUrgencyImage</texture>" +
                              "<visible>!string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>" +
                          "<control>" +
                            "<description>Text</description>" +
                            "<type>textbox</type>" +
                            "<id>1</id>" +
                            "<posX>1590</posX>" +
                            "<posY>180</posY>" +
                            "<width>297</width>" +
                            "<label>New Updates Available</label>" +
                            "<font>mediastream12tc</font>" +
                            "<visible>!string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>" +
                          "<control>" +
                            "<description>Text</description>" +
                            "<type>textbox</type>" +
                            "<id>1</id>" +
                            "<posX>1590</posX>" +
                            "<posY>180</posY>" +
                            "<width>297</width>" +
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
                            "<posX>1493</posX>" +
                            "<posY>285</posY>" +
                            "<width>297</width>" +
                            "<label>Available Updates</label>" +
                            "<font>mediastream10tc</font>" +
                          "</control>" +
                          "<control>" +
                            "<description>available windows updates</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>1493</posX>" +
                            "<posY>311</posY>" +
                            "<width>297</width>" +
                            "<label>#UpdateControl.AvailableUpdateCount</label>" +
                            "<font>mediastream10</font>          " +
                            "</control>" +
                          "<control>" +
                            "<description>Text available windows updates Size</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>1493</posX>" +
                            "<posY>353</posY>" +
                            "<width>297</width>" +
                            "<label>Size</label>" +
                            "<font>mediastream10tc</font>" +
                          "</control>" +
                          "<control>" +
                            "<description>available windows updates Size</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>1493</posX>" +
                            "<posY>378</posY>" +
                            "<width>297</width>" +
                            "<label>#UpdateControl.AvailableUpdateSize</label>" +
                            "<font>mediastream10</font>          " +
                          "</control>" +
                          "<control>" +
                            "<description>Text Update Time</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>1493</posX>" +
                            "<posY>420</posY>" +
                            "<width>297</width>" +
                            "<label>Update Time</label>" +
                            "<font>mediastream10tc</font>" +
                          "</control>" +
                          "<control>" +
                            "<description>Update.Time</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>1493</posX>" +
                            "<posY>446</posY>" +
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
                      "<posX>203</posX>" +
                      "<posY>15</posY>" +
                      "<width>315</width>" +
                      "<height>48</height>" +
                      "<texture>emailbackground.png</texture>" +
                      "<animation effect=\"fade\" time=\"200\" delay=\"100\">WindowOpen</animation>" +
                    "</control>" +
                    "<control>" +
                      "<description>New Email</description>" +
                      "<type>label</type>" +
                      "<posX>261</posX>" +
                      "<posY>20</posY>" +
                      "<label>#newEmail</label>" +
                      "<font>mediastream11c</font>" +
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
      //Controls to display recent Music Video overlay
      //
      if (isOverlay == isOverlayType.MusicVideos)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.musicVideos)
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
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.musicVideos)
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
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.musicVideos)
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
           isOverlay == isOverlayType.MusicVideos ||
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
