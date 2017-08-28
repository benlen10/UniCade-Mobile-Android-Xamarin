using System.Collections.Generic;
using System.Xml;

namespace UniCadeAndroid.Objects
{
    /// <summary>
    /// Represents the images for a single game
    /// </summary>
    public class GameImages
    {
        /// <summary>
        /// The artwork on the back of the box
        /// </summary>
        public string BoxartBack;

        /// <summary>
        /// The artwork on the front of the box
        /// </summary>
        public string BoxartFront;

        /// <summary>
        /// A list of all user submitted fanart
        /// </summary>
        public List<string> Fanart;

        /// <summary>
        /// A list of all user submitted banners
        /// </summary>
        public List<string> Banners;

        /// <summary>
        /// A list of all user submitted screenshots
        /// </summary>
        public List<string> Screenshots;

        /// <summary>
        /// Creates a new GameImages without any content.
        /// </summary>
        public GameImages()
        {
            Fanart = new List<string>();
            Banners = new List<string>();
            Screenshots = new List<string>();
        }

        /// <summary>
        /// Parses the XML node for all available images
        /// </summary>
        /// <param name="node">the XmlNode to parse</param>
        public void LoadFromNode(XmlNode node)
        {
            /*
            IEnumerator ienumImages = node.GetEnumerator();
            while (ienumImages.MoveNext())
            {
                XmlNode imageNode = (XmlNode)ienumImages.Current;

                if (imageNode.Name == "fanart")
                {
                    Fanart.Add(imageNode.FirstChild.InnerText);
                }
                else if (imageNode.Name == "banner")
                {
                    Banners.Add(imageNode.InnerText);
                }
                else if (imageNode.Name == "screenshot")
                {
                    Screenshots.Add(imageNode.FirstChild.InnerText);
                }
                else if (imageNode.Name == "boxart")
                {
                    if (imageNode.Attributes != null && imageNode.Attributes.GetNamedItem("side").InnerText == "front")
                    {
                        BoxartFront = imageNode.InnerText;
                    }
                    else
                    {
                        BoxartBack = imageNode.InnerText;
                    }
                }
            }
            */
        }
    }
}
