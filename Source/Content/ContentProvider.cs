using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace BluishFramework
{
    public static class ContentProvider
    {
        public static ContentManager Content { get; set; }

        public static ContentManager GetContentManager()
        {
            ContentManager content = new ContentManager(Content.ServiceProvider);
            content.RootDirectory = Content.RootDirectory;
            return content;
        }
    }
}