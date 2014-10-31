using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLoader.Test
{
    [TestClass]
    public class BlogLoaderTest
    {
        [TestMethod]
        public void CreateBlogPostFromWikiArticle_Test()
        {
            try
            {
                var loader = new BlogLoader();

                var result = loader.CreateBlogPostFromWikiArticle();

                Assert.AreEqual(result, 1);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.Fail();
            }


        }
        [TestMethod]
        public void Load100Articles_Test()
        {
            try
            {
                var loader = new BlogLoader();

                int rowCount = 0;
                for(int i = 1; i < 100; i++)
                {
                    var result = loader.CreateBlogPostFromWikiArticle();
                    rowCount = rowCount + result;
                }

                Assert.AreEqual(rowCount, 100);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.Fail();
            }
        }

    }
}
