using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;
using System.Web;

namespace StreamedMPEditor
{
  public partial class formStreamedMpEditor
  {
    void writeHorizonalMenu(int i, menuItem menItem, ref StringBuilder rawXML)
    {
      string acceleration = tbAcceleration.Text;
      string duration = tbDuration.Text;

      int onleft = 0;
      int onright = 0;

      switch (i)
      {
        case 0:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>button</type>");
          rawXML.AppendLine("<id>" + (menItem.id + 700).ToString() + "</id>");
          rawXML.AppendLine("<posX>0</posX>");
          rawXML.AppendLine("<posY>-45</posY>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");
          if (menItem.isDefault)
          {
            rawXML.AppendLine("<width>1920</width>");
            rawXML.AppendLine("<height>1080</height>");
          }
          else
          {
            rawXML.AppendLine("<width>480</width>");
            rawXML.AppendLine("<height>108</height>");
          }
          rawXML.AppendLine("<textureFocus>-</textureFocus>");
          rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");

          if (menItem.hyperlink == "196299")
            rawXML.AppendLine("<action>99</action>");
          else if (menItem.hyperlink == "196297")
            rawXML.AppendLine("<action>97</action>");
          else
            if (menItem.hyperlink == formStreamedMpEditor.musicSkinID && menItem.hyperlinkParameter != "false")
              rawXML.AppendLine("<hyperlink>504</hyperlink>");
            else
              rawXML.AppendLine("<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");

          //Plugin Parameter handling
          if (menItem.hyperlinkParameter != "false")
          {
            switch (menItem.hyperlink)
            {
              case onlineVideosSkinID:
                if (!IsOnlineVideosGroup(menItem.hyperlinkParameter))
                {
                  string search = string.IsNullOrEmpty(menItem.hyperlinkParameterSearch) ? string.Empty : "|search:" + HttpUtility.HtmlEncode(menItem.hyperlinkParameterSearch);
                  string category = string.IsNullOrEmpty(menItem.hyperlinkParameterCategory) ? string.Empty : "|category:" + HttpUtility.HtmlEncode(menItem.hyperlinkParameterCategory);
                  rawXML.AppendLine("<hyperlinkParameter>site:" + HttpUtility.HtmlEncode(menItem.hyperlinkParameter) + category + search + "|return:" + menItem.hyperlinkParameterOption + "</hyperlinkParameter>");
                }
                else
                {
                  rawXML.AppendLine("<hyperlinkParameter>group:" + HttpUtility.HtmlEncode(menItem.hyperlinkParameter.Substring(7)) + "|return:" + menItem.hyperlinkParameterOption + "</hyperlinkParameter>");
                }
                break;
              case movingPicturesSkinID:
                rawXML.AppendLine("<hyperlinkParameter>categoryid:" + HttpUtility.HtmlEncode(menItem.hyperlinkParameter) + "</hyperlinkParameter>");
                break;
              default:
                rawXML.AppendLine("<hyperlinkParameter>" + HttpUtility.HtmlEncode(menItem.hyperlinkParameter) + "</hyperlinkParameter>");
                break;
            }
          }

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

          if (menItem.subMenuLevel1ID > 0)
            rawXML.AppendLine("<onup>" + (menItem.subMenuLevel1ID + 1).ToString() + "</onup>");
          else
          {
            if (cbDisableExitMenu.Checked)
              rawXML.AppendLine("<onup>" + (menItem.id + 700).ToString() + "</onup>");
            else
              rawXML.AppendLine("<onup>" + (menItem.id + 600).ToString() + "01</onup>");
          }

          rawXML.AppendLine("<visible>control.isvisible(" + (menItem.id + 700).ToString() + ")</visible>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;

        case 1:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>button</type>");
          rawXML.AppendLine("<id>" + (menItem.id + 800).ToString() + "</id>");
          rawXML.AppendLine("<posX>0</posX>");
          rawXML.AppendLine("<posY>-45</posY>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textureFocus>-</textureFocus>");
          rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");

          if (menItem.hyperlink == "196299")
            rawXML.AppendLine("<action>99</action>");
          else if (menItem.hyperlink == "196297")
            rawXML.AppendLine("<action>97</action>");
          else
            if (menItem.hyperlink == formStreamedMpEditor.musicSkinID && menItem.hyperlinkParameter != "false")
              rawXML.AppendLine("<hyperlink>504</hyperlink>");
            else
              rawXML.AppendLine("<hyperlink>" + menItem.hyperlink.ToString() + "</hyperlink>");

          //Plugin Parameter handling
          if (menItem.hyperlinkParameter != "false")
          {
            switch (menItem.hyperlink)
            {
              case onlineVideosSkinID:
                if (!IsOnlineVideosGroup(menItem.hyperlinkParameter))
                {
                  string search = string.IsNullOrEmpty(menItem.hyperlinkParameterSearch) ? string.Empty : "|search:" + HttpUtility.HtmlEncode(menItem.hyperlinkParameterSearch);
                  string category = string.IsNullOrEmpty(menItem.hyperlinkParameterCategory) ? string.Empty : "|category:" + HttpUtility.HtmlEncode(menItem.hyperlinkParameterCategory);
                  rawXML.AppendLine("<hyperlinkParameter>site:" + HttpUtility.HtmlEncode(menItem.hyperlinkParameter) + category + search + "|return:" + menItem.hyperlinkParameterOption + "</hyperlinkParameter>");
                }
                else
                {
                  rawXML.AppendLine("<hyperlinkParameter>group:" + HttpUtility.HtmlEncode(menItem.hyperlinkParameter.Substring(7)) + "|return:" + menItem.hyperlinkParameterOption + "</hyperlinkParameter>");
                }
                break;
              case movingPicturesSkinID:
                rawXML.AppendLine("<hyperlinkParameter>categoryid:" + HttpUtility.HtmlEncode(menItem.hyperlinkParameter) + "</hyperlinkParameter>");
                break;
              default:
                rawXML.AppendLine("<hyperlinkParameter>" + HttpUtility.HtmlEncode(menItem.hyperlinkParameter) + "</hyperlinkParameter>");
                break;
            }
          }

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

          if (menItem.subMenuLevel1ID > 0)
            rawXML.AppendLine("<onup>" + (menItem.subMenuLevel1ID + 1).ToString() + "</onup>");
          else
          {
            if (cbDisableExitMenu.Checked)
              rawXML.AppendLine("<onup>" + (menItem.id + 800).ToString() + "</onup>");
            else
              rawXML.AppendLine("<onup>" + (menItem.id + 600).ToString() + "01</onup>");
          }

          rawXML.AppendLine("<visible>control.isvisible(" + (menItem.id + 800).ToString() + ")</visible>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;

        case 2:
          break;

        case 3:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>1680</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
          rawXML.AppendLine("<font>#labelFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

          if (menuItems.IndexOf(menItem) - 2 == -2)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 700).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) - 2 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 698).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;

        case 4:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>1200</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
          rawXML.AppendLine("<font>#labelFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

          if (menuItems.IndexOf(menItem) - 1 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 699).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;

        case 5:
          if (cbDropShadow.Checked)
          {
            rawXML.AppendLine("<control>");
            rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
            rawXML.AppendLine("<type>label</type>");
            rawXML.AppendLine("<posX>723</posX>");
            rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 3) + "</posY>");
            rawXML.AppendLine("<width>480</width>");
            rawXML.AppendLine("<height>108</height>");
            rawXML.AppendLine("<textcolor>" + dropShadowColor + "</textcolor>");
            rawXML.AppendLine("<font>#selectedFont</font>");
            rawXML.AppendLine("<align>center</align>");
            rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")</visible>");

            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
            rawXML.AppendLine("</control>");
          }
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>720</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
          rawXML.AppendLine("<font>#selectedFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

          rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 6:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>240</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
          rawXML.AppendLine("<font>#labelFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

          if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 7:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>0</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
          rawXML.AppendLine("<font>#labelFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

          if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 702).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "240,0" + quote + " end=" + quote + "-240,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 8:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>1680</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
          rawXML.AppendLine("<font>#labelFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

          if (menuItems.IndexOf(menItem) - 3 == -3)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 3].id + 800).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) - 3 == -2)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) - 3 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 3).ToString() + ")</visible>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 9:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>1200</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
          rawXML.AppendLine("<font>#labelFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

          if (menuItems.IndexOf(menItem) - 2 == -2)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) - 2 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 2).ToString() + ")</visible>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,0" + quote + " end=" + quote + "480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 10:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>1200</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
          rawXML.AppendLine("<font>#labelFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

          if (menuItems.IndexOf(menItem) - 1 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 1).ToString() + ")</visible>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 11:
          if (cbDropShadow.Checked)
          {
            rawXML.AppendLine("<control>");
            rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
            rawXML.AppendLine("<type>label</type>");
            rawXML.AppendLine("<posX>722</posX>");
            rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 2) + "</posY>");
            rawXML.AppendLine("<width>480</width>");
            rawXML.AppendLine("<height>108</height>");
            rawXML.AppendLine("<textcolor>" + dropShadowColor + "</textcolor>");
            rawXML.AppendLine("<font>#selectedFont</font>");
            rawXML.AppendLine("<align>center</align>");
            rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
            rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
            rawXML.AppendLine("</control>");

          }
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>720</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
          rawXML.AppendLine("<font>#selectedFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");
          rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 12:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>240</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
          rawXML.AppendLine("<font>#labelFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

          if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) + 1).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-480,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 13:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>0</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>480</width>");
          rawXML.AppendLine("<height>108</height>");
          rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
          rawXML.AppendLine("<font>#labelFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");

          if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 800).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) + 2).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "-720,0" + quote + " end=" + quote + "-240,0" + quote + " time=" + quote + duration + quote + " acceleration=" + quote + acceleration + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,450" + quote + " time=" + quote + "250" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
      }
    }
  }
}