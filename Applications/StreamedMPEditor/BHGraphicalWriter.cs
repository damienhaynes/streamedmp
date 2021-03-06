﻿using System;
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
    void writeGraphicalMenu(int i, menuItem menItem, ref StringBuilder rawXML)
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
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
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
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 2:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>-132</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");
          
          if (menuItems.IndexOf(menItem) + 4 == menuItems.Count + 1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) + 4 == menuItems.Count + 2)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 800).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) + 4 == menuItems.Count + 3)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[2].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 799 + 4).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"slide\" start=\"-315,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 3:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>1860</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) - 2 == -2)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 3].id + 700).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) - 2 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 700).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) - 2 == 0)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 697).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"slide\" start=\"315,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 4:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>1541</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) - 2 == -2)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 700).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) - 2 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 698).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"slide\" start=\"315,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 5:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>1221</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) - 1 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 700).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 699).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"slide\" start=\"315,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 6:
            rawXML.AppendLine("<control>");
            rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
            rawXML.AppendLine("<type>label</type>");
            rawXML.AppendLine("<posX>962</posX>");
            rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 228) + "</posY>");
            rawXML.AppendLine("<height>108</height>");
            rawXML.AppendLine("<textcolor>" + dropShadowColor + "</textcolor>");
            rawXML.AppendLine("<font>#labelFont</font>");
            rawXML.AppendLine("<align>center</align>");
            rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")</visible>");
            rawXML.AppendLine("<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
            rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
            rawXML.AppendLine("</control>");

            rawXML.AppendLine("<control>");
            rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
            rawXML.AppendLine("<type>label</type>");
            rawXML.AppendLine("<posX>960</posX>");
            rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 227) + "</posY>");
            rawXML.AppendLine("<height>108</height>");
            rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
            rawXML.AppendLine("<font>#labelFont</font>");
            rawXML.AppendLine("<align>center</align>");
            rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")</visible>");
            rawXML.AppendLine("<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
            rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
            rawXML.AppendLine("</control>");

          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>864</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");
          rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")</visible>");
         
          rawXML.AppendLine("<animation effect=\"zoom\" end=\"150,150\" time=\"600\" acceleration=\"-0.1\" reversible=\"false\">visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" start=\"357,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");

          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 7:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>507</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 701).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"zoom\" start=\"150,150\" end=\"100,100\" time=\"600\" acceleration=\"-0.1\" reversible=\"false\">visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" start=\"357,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 8:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>188</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 702).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"slide\"  start=\"315,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 9:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>188</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) + 4 == menuItems.Count + 1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 700).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) + 4 == menuItems.Count + 2)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 700).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) + 4 == menuItems.Count + 3)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[2].id + 700).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 699 + 4).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"slide\" end=\"-315,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 10:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>1541</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) - 3 == -3)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 3].id + 800).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) - 3 == -2)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) - 3 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 3).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"slide\" end=\"315,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 11:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>1221</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) - 2 == -2)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 2].id + 800).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) - 2 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 2).ToString() + ")</visible>");
          
          rawXML.AppendLine("<animation effect=\"slide\" start=\"0,0\" end=\"357,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 12:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>1221</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) - 1 == -1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[menuItems.Count - 1].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) - 1).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"zoom\" start=\"150,150\" end=\"100,100\" time=\"600\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" start=\"-315,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 13:
            rawXML.AppendLine("<control>");
            rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
            rawXML.AppendLine("<type>label</type>");
            rawXML.AppendLine("<posX>962</posX>");
            rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 228) + "</posY>");
            rawXML.AppendLine("<height>108</height>");
            rawXML.AppendLine("<textcolor>" + dropShadowColor + "</textcolor>");
            rawXML.AppendLine("<font>#labelFont</font>");
            rawXML.AppendLine("<align>center</align>");
            rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
            rawXML.AppendLine("<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
            rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
            rawXML.AppendLine("</control>");

            rawXML.AppendLine("<control>");
            rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
            rawXML.AppendLine("<type>label</type>");
            rawXML.AppendLine("<posX>960</posX>");
            rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 227) + "</posY>");
            rawXML.AppendLine("<height>108</height>");
            rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
            rawXML.AppendLine("<font>#labelFont</font>");
            rawXML.AppendLine("<align>center</align>");
            rawXML.AppendLine("<label>" + HttpUtility.HtmlEncode(menItem.name) + "</label>");
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");
            rawXML.AppendLine("<animation effect=\"fade\" delay=\"600\" time=\"300\">Visible</animation>");
            rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
            rawXML.AppendLine("</control>");

          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>864</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");
          rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 800).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"zoom\" end=\"150,150\" time=\"600\" acceleration=\"-0.1\" reversible=\"false\">visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" start=\"-357,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 14:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>507</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) + 1 == menuItems.Count)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) + 1).ToString() + ")</visible>");
          
          rawXML.AppendLine("<animation effect=\"slide\" start=\"-315,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
        case 15:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>" + HttpUtility.HtmlEncode(menItem.name) + i.ToString() + "</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<posX>188</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
          rawXML.AppendLine("<width>192</width>");
          rawXML.AppendLine("<height>192</height>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<texture>" + menItem.buttonTexture + "</texture>");

          if (menuItems.IndexOf(menItem) + 2 == menuItems.Count + 1)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[1].id + 800).ToString() + ")</visible>");
          else if (menuItems.IndexOf(menItem) + 2 == menuItems.Count)
            rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[0].id + 800).ToString() + ")</visible>");
          else
            rawXML.AppendLine("<visible>Control.HasFocus(" + ((menItem.id + 800) + 2).ToString() + ")</visible>");

          rawXML.AppendLine("<animation effect=\"slide\" start=\"-315,0\" time=\"" + duration + "\" acceleration=\"" + acceleration + "\" reversible=\"false\">Visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,450\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
      }
    }
  }
}