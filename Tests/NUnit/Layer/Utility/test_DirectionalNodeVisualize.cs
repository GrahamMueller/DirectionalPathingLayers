using NUnit.Framework;
using System.Drawing;
using System.IO;

namespace DirectionalPathingLayers.Tests
{
    class test_DirectionalNodeVisualize
    {
        bool updateFiles = false;

        [Platform("Win", Reason = "Only runs on Windows because of drawing module ")]
        [Test]
        public void Test_Tiles()
        {
            string projectRoot = this.GetRootProjectFolder();
            string testImage_5 = Path.Combine(new string[] { "Tests", "NUnit", "Layer", "Utility", "visualize_tiles_5.png" });
            testImage_5 = Path.GetFullPath(Path.Combine(projectRoot, testImage_5));
            string testImage_10 = Path.Combine(new string[] { "Tests", "NUnit", "Layer", "Utility", "visualize_tiles_10.png" });
            testImage_10 = Path.GetFullPath(Path.Combine(projectRoot, testImage_10));

            MapDirectionalLayer mapLayer = new MapDirectionalLayer(6, 6, new DirectionalNode(new int[] { 1, 1, 1, 1, 1, 1 }));
            DirectionalNodeVisualize vis = new DirectionalNodeVisualize(mapLayer.DirectionLayer.directionalNodes, 5);
            if (this.updateFiles)
            {
                vis.SaveAsPng(testImage_5);
            }
            else
            {
                Bitmap loadBitmap = new Bitmap(testImage_5);
                Assert.IsTrue(this.CompareBitmaps(loadBitmap, vis.GetBitmap()));
            }

            //Test larger size.
            vis = new DirectionalNodeVisualize(mapLayer.DirectionLayer.directionalNodes, 10);
            if (this.updateFiles)
            {
                vis.SaveAsPng(testImage_10);
            }
            else
            {
                Bitmap loadBitmap = new Bitmap(testImage_10);
                Assert.IsTrue(this.CompareBitmaps(loadBitmap, vis.GetBitmap()));
            }
        }


        //Set other directions to empty, set working direction to color, test for color
        [Platform("Win", Reason = "Only runs on Windows because of drawing module ")]
        [Test]
        public void Test_Directions()
        {
            string projectRoot = this.GetRootProjectFolder();
            string testImage = Path.Combine(new string[] { "Tests", "NUnit", "Layer", "Utility", "visualize_directions.png" });
            testImage = Path.GetFullPath(Path.Combine(projectRoot, testImage));


            MapDirectionalLayer mapLayer = new MapDirectionalLayer(6, 6, new DirectionalNode(new int[] { 1, 1, 1, 1, 1, 1 }));
            Color[] lineColors = new Color[6] { Color.Black, Color.White, Color.Red, Color.Blue, Color.Teal, Color.Aqua };
            DirectionalNodeVisualize vis = new DirectionalNodeVisualize(mapLayer.DirectionLayer.directionalNodes, 10, Color.Orange, lineColors);
            if (this.updateFiles)
            {
                vis.SaveAsPng(testImage);
            }
            else
            {
                Bitmap loadBitmap = new Bitmap(testImage);
                Assert.IsTrue(this.CompareBitmaps(loadBitmap, vis.GetBitmap()));
            }
        }


        //Set background tiles
        [Platform("Win", Reason = "Only runs on Windows because of drawing module ")]
        [Test]
        public void Test_Background()
        {
            string projectRoot = this.GetRootProjectFolder();

            string testImage = Path.Combine(new string[] { "Tests", "NUnit", "Layer", "Utility", "visualize_background.png" });
            testImage = Path.GetFullPath(Path.Combine(projectRoot, testImage));


            MapDirectionalLayer mapLayer = new MapDirectionalLayer(6, 6, new DirectionalNode(new int[] { 0, 1, 0, 1, 0, 1 }));
            DirectionalNodeVisualize vis = new DirectionalNodeVisualize(mapLayer.DirectionLayer.directionalNodes, 10);

            for (int x = 0; x < mapLayer.DirectionLayer.directionalNodes.GetLength(0); x+=2)
            {
                for (int y = 0; y < mapLayer.DirectionLayer.directionalNodes.GetLength(1); y+=2)
                {
                    vis.SetBackgroundTileColor(x, y, Color.Red);
                }
            }

            if (this.updateFiles)
            {
                vis.SaveAsPng(testImage);
            }
            else
            {
                Bitmap loadBitmap = new Bitmap(testImage);
                Assert.IsTrue(this.CompareBitmaps(loadBitmap, vis.GetBitmap()));
            }
        }

        //Hacky way to work backwards from wherever the DLL for tests ends up.
        string GetRootProjectFolder()
        {
            string cwd = Directory.GetCurrentDirectory();

            while (!this.IsProjectRoot(cwd))
            {
                cwd = Directory.GetParent(cwd).FullName;
            }

            return cwd;
        }


        bool IsProjectRoot(string path)
        {
            string[] projectRootFolders = new string[] { "Tests", "Runtime" };

            foreach (string checkFolder in projectRootFolders)
            {
                DirectoryInfo di = new DirectoryInfo(path);
                DirectoryInfo[] directories = di.GetDirectories(checkFolder, SearchOption.TopDirectoryOnly);
                if (directories.Length == 0) { return false; }
            }

            return true;
        }


        bool CompareBitmaps(Bitmap a, Bitmap b)
        {
            if (a.Width != b.Width || a.Height != b.Height) { return false; }

            for (int x = 0; x < a.Width; ++x)
            {
                for (int y = 0; y < a.Height; ++y)
                {
                    if (a.GetPixel(x, y) != b.GetPixel(x, y)) { return false; }
                }
            }
            return true;
        }


    }
}
