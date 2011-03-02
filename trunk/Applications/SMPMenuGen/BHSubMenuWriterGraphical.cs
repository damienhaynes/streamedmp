using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;

namespace SMPMenuGen
{
  public partial class GenerateMenu
  {
    string bhSubMenuWriterGraphical()
    {
      level1LateralBladeVisible = "control.isvisible(";
      level2LateralBladeVisible = "control.isvisible(";
      foreach (menuItem menItem in menuItems)
      {
        if (menItem.subMenuLevel1.Count > 0)
        {
          level1LateralBladeVisible += menItem.subMenuLevel1ID.ToString() + ")|control.isvisible(";

          if (menItem.subMenuLevel2.Count > 0)
            level1LateralBladeVisible += (menItem.subMenuLevel1ID + 100).ToString() + ")|control.isvisible(";

          writeSubMenuLevel1Graphical(menItem);
        }
        if (menItem.subMenuLevel2.Count > 0)
        {
          level2LateralBladeVisible += (menItem.subMenuLevel1ID + 100).ToString() + ")|control.isvisible(";
          writeSubMenuLevel2Graphical(menItem);
        }
      }
      localxml += "<!--             End of Submenu Code            -->";
      return localxml;
    }
    /// <summary>
    /// Write out Sunmenu Level 1
    /// </summary>
    /// <param name="parentMenu"></param>
    /// <returns></returns>
    string writeSubMenuLevel1Graphical(formStreamedMpEditor.menuItem parentMenu)
    {
      string dummyFocusControls = "Control.HasFocus(";

      for (int i = 0; i < parentMenu.subMenuLevel1.Count; i++)
      {
        if (i < (parentMenu.subMenuLevel1.Count - 1))
          dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")|Control.HasFocus(";
        else
          if (parentMenu.subMenuLevel2.Count == 0)
            dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")";
          else
            dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")|Control.HasFocus(";
      }
      if (parentMenu.subMenuLevel2.Count > 0)
      {
        for (int i = 0; i < parentMenu.subMenuLevel2.Count; i++)
        {
          if (i < (parentMenu.subMenuLevel2.Count - 1))
            dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")|Control.HasFocus(";
          else
            dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")";
        }
      }
      //
      //work out where to start the menu
      //
      int menuStart = (int.Parse(txtMenuPos.Text) - (50 * parentMenu.subMenuLevel1.Count) - 20);

      localxml += "<control>" +
                      "<description>dummy for lateral blade visibility</description>" +
                      "<type>label</type>" +
                      "<id>" + parentMenu.subMenuLevel1ID.ToString() + "</id>" +
                      "<label></label>" +
                      "<visible>" + dummyFocusControls + "</visible>" +
                    "</control>" +
                    "<control>" +
                      "<type>group</type>" +
                      "<description>group element</description>" +
                      "<animation effect=\"fade\" time=\"200\" start=\"0\" end=\"100\">visible</animation>" +
                      "<animation effect=\"fade\" time=\"200\" start=\"100\" end=\"0\">hidden</animation>" +
                      "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                      "<animation effect=\"fade\" start=\"0\"  end=\"100\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                      "<animation effect=\"zoom\" start=\"10,10\" end=\"100,100\" center=\"640," + (int.Parse(txtMenuPos.Text) - 255 + 125).ToString() + "\" time=\"200\">Visible</animation>" +
                      "<animation effect=\"zoom\" start=\"100,100\" end=\"10,10\" center=\"640," + (int.Parse(txtMenuPos.Text) - 255 + 125).ToString() + "\" time=\"200\">Hidden</animation>" +
                      "<posX>480</posX>" +
                      "<posY>" + menuStart.ToString() + "</posY>" +
                      "<width>320</width>" +
                      "<height>405</height>" +
                      "<dimColor>ffffffff</dimColor>" +
                      "<layout>StackLayout</layout>";

      for (int j = 0; j < parentMenu.subMenuLevel1.Count; j++)
      {
        onup = (parentMenu.subMenuLevel1ID + (j)).ToString();
        ondown = (parentMenu.subMenuLevel1ID + (j + 2)).ToString();
        if (j == 0)
        {
          onup = (parentMenu.subMenuLevel1ID + parentMenu.subMenuLevel1.Count).ToString();
          ondown = (parentMenu.subMenuLevel1ID + (j + 2)).ToString();
        }

        if (j == (parentMenu.subMenuLevel1.Count - 1))
        {
          onup = (parentMenu.subMenuLevel1ID + j).ToString();
          ondown = (parentMenu.id + 900).ToString();
        }

        localxml += "<control Style=\"submenubutton\">" +
                "<description>SUB ITEM " + j.ToString() + "</description>" +
                "<type>button</type>" +
                "<id>" + (parentMenu.subMenuLevel1ID + (j + 1)).ToString() + "</id>" +
                "<label>" + parentMenu.subMenuLevel1[j].displayName + "</label>";

        if (parentMenu.subMenuLevel1[j].hyperlink == "196299")
          localxml += "<action>99</action>";
        else if (parentMenu.subMenuLevel1[j].hyperlink == "196297")
          localxml += "<action>97</action>";
        else if (parentMenu.subMenuLevel1[j].hyperlink == "196250")
          localxml += "<action>196250</action>";
        else
          if (parentMenu.subMenuLevel1[j].hyperlink == formStreamedMpEditor.musicSkinID && parentMenu.subMenuLevel1[j].hyperlinkParameter != "false")
            localxml += "<hyperlink>504</hyperlink>";
          else
            localxml += "<hyperlink>" + parentMenu.subMenuLevel1[j].hyperlink + "</hyperlink>";

        //Plugin Parameter handling
        if (parentMenu.subMenuLevel1[j].hyperlinkParameter != "false")
        {
          switch (parentMenu.subMenuLevel1[j].hyperlink)
          {
            case onlineVideosSkinID:
              localxml += "<hyperlinkParameter>site:" + parentMenu.subMenuLevel1[j].hyperlinkParameter + "|return:" + parentMenu.subMenuLevel1[j].hyperlinkParameterOption + "</hyperlinkParameter>";
              break;
            default:
              localxml += "<hyperlinkParameter>" + parentMenu.subMenuLevel1[j].hyperlinkParameter + "</hyperlinkParameter>";
              break;
          }
        }

        localxml += "<onleft>" + (parentMenu.id + 900).ToString() + "</onleft>" +
                    "<onright>" + (parentMenu.id + 900).ToString() + "</onright>" +
          //"<onright>" + (parentMenu.subMenuLevel1ID + (1 + isSecondLevel)).ToString() + "</onright>" +
                    "<ondown>" + ondown + "</ondown>" +
                    "<onup>" + onup + "</onup>" +
                    "<visible allowhiddenfocus=\"true\">control.isvisible(" + parentMenu.subMenuLevel1ID.ToString() + ")</visible>" +
                  "</control>";
      }
      localxml += "</control>";

      // Main Level menu Indicator
      subArrowVisible = "control.isvisible(11111)|Control.HasFocus(";
      foreach (menuItem item in menuItems)
      {
        if (item.subMenuLevel1ID != 0)
          subArrowVisible += (item.id + 700).ToString() + ")|control.hasfocus(" + (item.id + 800).ToString() + ")|control.hasfocus(" + (item.id + 900).ToString() + ")|control.hasfocus(";
      }
      subArrowVisible = subArrowVisible.Substring(0, (subArrowVisible.Length - 18));

      localxml += "<control>" +
                  "<description>Sub Menu Indicator (Main)</description>" +
                  "<type>image</type>" +
                  "<posX>630</posX>" +
                  "<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 205).ToString() + "</posY>" +
                  "<align>center</align>" +
                  "<width>24</width>" +
                  "<height>24</height>" +
                  "<visible>" + subArrowVisible + "</visible>" +
                  "<colorDiffuse>77fffffff</colorDiffuse>" +
                  "<texture>submenuarrow.png</texture>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"800\" reversible=\"false\">WindowOpen</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"250\" delay=\"700\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" end=\"0,300\" time=\"250\" acceleration=\" -0.1\" reversible=\"false\">windowclose</animation>" +
                "</control>";

      // 2nd Level Menu Indicator
      //subArrowVisible = "control.isvisible(22222)|control.hasfocus(";
      //foreach (menuItem item in menuItems)
      //{
      //  if (item.subMenuLevel1ID != 0)
      //    if (item.subMenuLevel2.Count > 0)
      //    {
      //      for (int i = 0; i < item.subMenuLevel1.Count; i++)
      //      {
      //        subArrowVisible += (item.subMenuLevel1ID + (i + 1)).ToString() + ")|control.hasfocus(";
      //      }
      //    }
      //}
      //subArrowVisible = subArrowVisible.Substring(0, (subArrowVisible.Length - 18));
      //localxml += "<control>" +
      //            "<description>Sub Menu Indicator (Level1)</description>" +
      //            "<type>image</type>" +
      //            "<posX>750</posX>" +
      //            "<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 165).ToString() + "</posY>" +
      //            "<align>right</align>" +
      //            "<width>16</width>" +
      //            "<height>16</height>" +
      //            "<visible>" + subArrowVisible + "</visible>" +
      //            "<colorDiffuse>77fffffff</colorDiffuse>" +
      //            "<texture>arrowrightfo.png</texture>" +
      //            "<animation effect=\"slide\" start=\"-400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
      //            "<animation effect=\"slide\" end=\"-400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
      //          "</control>";


      return localxml;
    }


    /// <summary>
    /// Write out Submenu Level 2
    /// </summary>
    /// <param name="parentMenu"></param>
    /// <returns></returns>
    string writeSubMenuLevel2Graphical(formStreamedMpEditor.menuItem parentMenu)
    {
      string dummyFocusControls = "Control.HasFocus(";

      if (menuStyle != chosenMenuStyle.verticalStyle && horizontalContextLabels.Checked)
        conextOffsett = 17;

      for (int i = 0; i < parentMenu.subMenuLevel2.Count; i++)
      {
        if (i < (parentMenu.subMenuLevel2.Count - 1))
          dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")|Control.HasFocus(";
        else
          dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")";
      }


      localxml += "<control>" +
                      "<description>dummy for lateral blade visibility</description>" +
                      "<type>label</type>" +
                      "<id>" + (parentMenu.subMenuLevel1ID + 100).ToString() + "</id>" +
                      "<label></label>" +
                      "<visible>" + dummyFocusControls + "</visible>" +
                    "</control>" +
                    "<control>" +
                      "<type>group</type>" +
                      "<description>group element</description>" +
                      "<animation effect=\"fade\" time=\"200\" start=\"0\" end=\"100\">visible</animation>" +
                      "<animation effect=\"fade\" time=\"200\" start=\"100\" end=\"0\">hidden</animation>" +
                      "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                      "<animation effect=\"fade\" start=\"0\"  end=\"100\" time=\"400\" delay=\"200\">WindowOpen</animation>" +
                      "<animation effect=\"zoom\" start=\"10,10\" end=\"100,100\" center=\"765," + (int.Parse(txtMenuPos.Text) - 255 + 125).ToString() + "\" time=\"200\">Visible</animation>" +
                      "<animation effect=\"zoom\" start=\"100,100\" end=\"10,10\" center=\"765," + (int.Parse(txtMenuPos.Text) - 255 + 125).ToString() + "\" time=\"200\">Hidden</animation>" +
                      "<posY>" + (int.Parse(txtMenuPos.Text) - 355 - conextOffsett).ToString() + "</posY>" +
                      "<posX>768</posX>" +
                      "<width>230</width>" +
                      "<height>405</height>" +
                      "<dimColor>ffffffff</dimColor>" +
                      "<layout>StackLayout</layout>";

      for (int j = 0; j < parentMenu.subMenuLevel2.Count; j++)
      {
        onup = (parentMenu.subMenuLevel1ID + (j + 100)).ToString();
        ondown = (parentMenu.subMenuLevel1ID + (j + 102)).ToString();
        if (j == 0)
        {
          onup = (parentMenu.subMenuLevel1ID + parentMenu.subMenuLevel2.Count).ToString();
          ondown = (parentMenu.subMenuLevel1ID + (j + 102)).ToString();
        }

        if (j == (parentMenu.subMenuLevel2.Count - 1))
        {
          onup = (parentMenu.subMenuLevel1ID + 100 + j).ToString();
          ondown = (parentMenu.subMenuLevel1ID + 101).ToString();
        }

        localxml += "<control Style=\"submenubutton\">" +
                "<description>SUB ITEM " + j.ToString() + "</description>" +
                "<type>button</type>" +
                "<id>" + (parentMenu.subMenuLevel1ID + (j + 101)).ToString() + "</id>" +
                "<label>" + parentMenu.subMenuLevel2[j].displayName + "</label>" +
                "<width>246</width>";

        if (parentMenu.subMenuLevel2[j].hyperlink == "196299")
          localxml += "<action>99</action>";
        else if (parentMenu.subMenuLevel2[j].hyperlink == "196297")
          localxml += "<action>97</action>";
        else if (parentMenu.subMenuLevel2[j].hyperlink == "196250")
          localxml += "<action>196250</action>";
        else
          if (parentMenu.subMenuLevel2[j].hyperlink == formStreamedMpEditor.musicSkinID && parentMenu.subMenuLevel2[j].hyperlinkParameter != "false")
            localxml += "<hyperlink>504</hyperlink>";
          else
            localxml += "<hyperlink>" + parentMenu.subMenuLevel2[j].hyperlink + "</hyperlink>";

        //Plugin Parameter handling
        if (parentMenu.subMenuLevel2[j].hyperlinkParameter != "false")
        {
          switch (parentMenu.subMenuLevel2[j].hyperlink)
          {
            case onlineVideosSkinID:
              localxml += "<hyperlinkParameter>site:" + parentMenu.subMenuLevel2[j].hyperlinkParameter + "|return:" + parentMenu.subMenuLevel2[j].hyperlinkParameterOption + "</hyperlinkParameter>";
              break;
            default:
              localxml += "<hyperlinkParameter>" + parentMenu.subMenuLevel2[j].hyperlinkParameter + "</hyperlinkParameter>";
              break;
          }
        }

        localxml += "<onleft>" + (parentMenu.subMenuLevel1ID + 1).ToString() + "</onleft>" +
                 "<onright>" + (parentMenu.subMenuLevel1ID + (j + 101)).ToString() + "</onright>" +
                 "<ondown>" + ondown + "</ondown>" +
                 "<onup>" + onup + "</onup>" +
                 "<visible allowhiddenfocus=\"true\">control.isvisible(" + (parentMenu.subMenuLevel1ID + 100).ToString() + ")</visible>" +
               "</control>";
      }
      localxml += "</control>";
      return localxml;
    }
  }
}
