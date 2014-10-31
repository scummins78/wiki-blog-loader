using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security.AntiXss;

using Wiki.Repository;
using Wiki.Repository.Models;
using DataRepository.Repository;
using DataRepository.Repository.EF;
using DataRepository.Models;


namespace DataLoader
{
    public class BlogLoader
    {
        private readonly IBlogRepository blogRepository;
        private readonly WikiRepository wikiRepository;

        public BlogLoader()
        {
            blogRepository = new DBRepository(new BlogPostContext());
            wikiRepository = new WikiRepository();
        }

        /// <summary>
        /// retrieves a wiki article and converts it to a blog post
        /// </summary>
        public int CreateBlogPostFromWikiArticle()
        {
            // get a wiki article
            var wikiArticle = new WikiArticle();
            wikiArticle = wikiRepository.GetRandomArticle();
 
            // make sure the article has content before continuing
            while (wikiArticle.extract.Length == 0)
            {
                wikiArticle = wikiRepository.GetRandomArticle();
            }

            // create a blog post object from the wiki article
            var newPost = new BlogPost();
            newPost.ParseWikiArticle(wikiArticle);

            // store blog post
            return blogRepository.InsertOrUpdate(newPost);
        }
    }

    #region extensions

    public static class BlogExtensions
    {

        private static readonly DateTime dateStart = new DateTime(2010, 1, 1);
        private static readonly Random gen = new Random();

        public static DateTime RandomDay()
        {
            int range = (DateTime.Today - dateStart).Days;           
            return dateStart.AddDays(gen.Next(range));
        }

        public static void ParseWikiArticle(this BlogPost post, WikiArticle article)
        {
            post.Author = "Shaun Cummins";
            post.AuthorId = Guid.NewGuid().ToString();
            post.BlogText = article.extract;
            post.Category = "Brewing";
            post.DateTimePosted = RandomDay();
            post.MainImageId = "CfDkd7A";
            post.Title = article.title.Replace(":", "").Replace(",", "").Replace("/", " ");
            post.UrlTitle = post.Title.Replace(" ", "-").ToLower();

            ScrubPostForStorage(post);
        }

        private static void ScrubPostForStorage(BlogPost post)
        {
            post.Author = AntiXssEncoder.HtmlEncode(post.Author, true);
            post.BlogText = AntiXssEncoder.HtmlEncode(post.BlogText, true);
            post.Title = AntiXssEncoder.HtmlEncode(post.Title, true);
            post.MainImageId = AntiXssEncoder.UrlEncode(post.MainImageId);
        }
    }

    #endregion
}
