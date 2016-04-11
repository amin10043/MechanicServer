using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Services;

namespace Service
{
    [ToolboxItem(false), WebService(Namespace = "http://tempuri.org/"), WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class MyService : WebService
    {
        private MechanicalDataContext context = new MechanicalDataContext();

        private int recordCount = 5;

        private User user = new User();

        [WebMethod]
        public string connect(string imei)
        {
            return "";
        }

        [WebMethod]
        public string Deleting(string tableName, string param)
        {
            string text = "";
            string text2 = "";
            string text3 = "";
            string[] array = param.Split(new char[]
			{
				'-'
			});
            string text4 = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null && array[i].Contains(":"))
                {
                    text = array[i].Substring(0, array[i].IndexOf(":"));
                    text4 = array[i].Substring(array[i].IndexOf(":") + 1, array[i].Length - array[i].IndexOf(":") - 1);
                    try
                    {
                        int num = Convert.ToInt32(text4);
                        text2 = text4;
                    }
                    catch (Exception)
                    {
                        text2 = "'" + text4 + "'";
                    }
                    string text5 = text3;
                    text3 = string.Concat(new string[]
					{
						text5,
						text,
						"=",
						text2,
						" AND "
					});
                }
            }
            text3 = text3.Remove(text3.LastIndexOf("AND"), 3);
            return this.context.ExecuteCommand("Delete From " + tableName + " WHERE " + text3, new object[0]).ToString();
        }

        [WebMethod]
        public string DeletingRecord(string tableName, string Id)
        {
            int num = this.context.ExecuteCommand("Delete From " + tableName + " WHERE Id =" + Id, new object[0]);
            num = this.context.ExecuteCommand(string.Concat(new string[]
			{
				"Delete From LikeIn",
				tableName,
				" WHERE ",
				tableName,
				"Id =",
				Id
			}), new object[0]);
            num = this.context.ExecuteCommand(string.Concat(new string[]
			{
				"Delete From CommentIn",
				tableName,
				" WHERE ",
				tableName,
				"Id =",
				Id
			}), new object[0]);
            if (tableName.Contains("Comment"))
            {
                num = this.context.ExecuteCommand(string.Concat(new string[]
    			{
			        "Delete From LikeInComment",
			        " WHERE ",
			        "CommentId =",
			        Id
                }), new object[0]);
                num = this.context.ExecuteCommand(string.Concat(new string[]
			    {
				    "Delete From ",
				    tableName,
				    " WHERE ",
				    "CommentId =",
				    Id
                }), new object[0]);

            }

            return num;
        }

        [WebMethod]
        public string getAllAnad(string fromDate, string endDate, int isRefresh)
        {
            List<Anad> list = null;
            string text = "";
            text = " Anad***Id^^^UserId^^^ObjectId^^^Date^^^ TypeId^^^ ProvinceId^^^ Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Anads
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<Anad>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Anads
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Anad>();
            }
            else
            {
                list = (from x in this.context.Anads
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Anad>();
            }
            string result;
            if (list == null || list.Count<Anad>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Anad>().ModifyDate,
					"***",
					list.LastOrDefault<Anad>().ModifyDate,
					"***"
				});
                foreach (Anad current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
                        current.UserId,
                        "^^^",
						current.ObjectId,
						"^^^",
						current.Date,
						"^^^",
						current.TypeId,
						"^^^",
						current.ProvinceId,
						"^^^0***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllCmtInPaper(string fromDate, string endDate, int isRefresh)
        {
            List<CommentInPaper> list = null;
            string text = "";
            text = " CmtInPaper***Id^^^Desk^^^PaperId^^^UserId^^^Date^^^ CommentId^^^ Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.CommentInPapers
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<CommentInPaper>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.CommentInPapers
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<CommentInPaper>();
            }
            else
            {
                list = (from x in this.context.CommentInPapers
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<CommentInPaper>();
            }
            string result;
            if (list == null || list.Count<CommentInPaper>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<CommentInPaper>().ModifyDate,
					"***",
					list.LastOrDefault<CommentInPaper>().ModifyDate,
					"***"
				});
                foreach (CommentInPaper current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Desk,
						"^^^",
						current.PaperId,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"^^^",
						current.CommentId,
						"^^^0***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllCommentInFroum(string fromDate, string endDate, int isRefresh)
        {
            List<CommentInFroum> list = null;
            string text = "";
            text = " CommentInFroum***Id^^^Desk^^^FroumId^^^UserId^^^Date^^^CommentId^^^NumofLike^^^NumofDisLike^^^Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.CommentInFroums
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<CommentInFroum>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.CommentInFroums
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<CommentInFroum>();
            }
            else
            {
                list = (from x in this.context.CommentInFroums
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<CommentInFroum>();
            }
            string result;
            if (list == null || list.Count<CommentInFroum>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<CommentInFroum>().ModifyDate,
					"***",
					list.LastOrDefault<CommentInFroum>().ModifyDate,
					"***"
				});
                foreach (CommentInFroum current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.ID,
						"^^^",
						current.Desk,
						"^^^",
						current.FroumId,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"^^^",
						current.CommentId,
						"^^^",
						current.NumofLike,
						"^^^",
						current.NumofDisLike,
						"^^^0***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllCommentInObject(string fromDate, string endDate, int isRefresh)
        {
            List<CommentInObject> list = null;
            string text = "";
            text = " CommentInObject***Id^^^Desk^^^ObjectId^^^UserId^^^Date^^^ CommentId ^^^ Seen ***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.CommentInObjects
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<CommentInObject>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.CommentInObjects
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<CommentInObject>();
            }
            else
            {
                list = (from x in this.context.CommentInObjects
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<CommentInObject>();
            }
            string result;
            if (list == null || list.Count<CommentInObject>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<CommentInObject>().ModifyDate,
					"***",
					list.LastOrDefault<CommentInObject>().ModifyDate,
					"***"
				});
                foreach (CommentInObject current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Desk,
						"^^^",
						current.ObjectId,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"^^^",
						current.CommentId,
						"^^^0***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllCommentInPost(string fromDate, string endDate, int isRefresh)
        {
            List<CommentInPost> list = null;
            string text = "";
            text = " CommentInPost***Id^^^Description^^^PostId^^^UserId^^^Date^^^ CommentId***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.CommentInPosts
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<CommentInPost>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.CommentInPosts
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<CommentInPost>();
            }
            else
            {
                list = (from x in this.context.CommentInPosts
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<CommentInPost>();
            }
            string result;
            if (list == null || list.Count<CommentInPost>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<CommentInPost>().ModifyDate,
					"***",
					list.LastOrDefault<CommentInPost>().ModifyDate,
					"***"
				});
                foreach (CommentInPost current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Description,
						"^^^",
						current.PostId,
						"^^^",
						current.UserId,
						"^^^",
						current.ModifyDate,
						"^^^",
						current.CommentId,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllLikeInPost(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInPost> list = null;
            string text = "";
            text = " LikeInPost***Id^^^UserId^^^PostId^^^Date^^^CommentId***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.LikeInPosts
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<LikeInPost>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.LikeInPosts
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInPost>();
            }
            else
            {
                list = (from x in this.context.LikeInPosts
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInPost>();
            }
            string result;
            if (list == null || list.Count<LikeInPost>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<LikeInPost>().ModifyDate,
					"***",
					list.LastOrDefault<LikeInPost>().ModifyDate,
					"***"
				});
                foreach (LikeInPost current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.PostId,
						"^^^",
						current.ModifyDate,
						"^^^",
						current.CommentId,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllLikeInCommentPost(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInCommentPost> list = null;
            string text = "";
            text = " LikeInCommentPost***Id^^^CommentId^^^UserId^^^IsLike^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.LikeInCommentPosts
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<LikeInCommentPost>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.LikeInCommentPosts
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInCommentPost>();
            }
            else
            {
                list = (from x in this.context.LikeInCommentPosts
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInCommentPost>();
            }
            string result;
            if (list == null || list.Count<LikeInCommentPost>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<LikeInCommentPost>().ModifyDate,
					"***",
					list.LastOrDefault<LikeInCommentPost>().ModifyDate,
					"***"
				});
                foreach (LikeInCommentPost current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.CommentId,
						"^^^",
						current.UserId,
						"^^^",
						current.IsLike,
						"^^^",
						current.ModifyDate,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllFroum(string fromDate, string endDate, int isRefresh)
        {
            List<Froum> list = null;
            string text = "";
            text = " Froum***Id^^^Title^^^Description^^^UserId^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Froums
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<Froum>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Froums
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Froum>();
            }
            else
            {
                list = (from x in this.context.Froums
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Froum>();
            }
            string result;
            if (list == null || list.Count<Froum>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Froum>().ModifyDate,
					"***",
					list.LastOrDefault<Froum>().ModifyDate,
					"***"
				});
                foreach (Froum current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Title,
						"^^^",
						current.Description,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public List<byte[]> getAllImage(string tableName, int id, string fromDate1, string fromDate2, string fromDate3)
        {
            return new List<byte[]>(3)
			{
				this.getObject1Image(tableName, id, fromDate1),
				this.getObject2Image(tableName, id, fromDate2),
				this.getObject3Image(tableName, id, fromDate3)
			};
        }

        [WebMethod]
        public string getAllImageServerDate(string fromDate, string endDate, int isRefresh)
        {
            string result = "";
            if (fromDate != null && fromDate != "")
            {
                Object @object = (from x in this.context.Objects
                                  where x.Id == Convert.ToInt32(fromDate)
                                  select x).FirstOrDefault<Object>();
                result = string.Concat(new string[]
				{
					@object.Image1Date,
					"-",
					@object.Image2Date,
					"-",
					@object.Image3Date,
					"-"
				});
            }
            return result;
        }

        [WebMethod]
        public string getAllLikeInComment(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInComment> list = null;
            string text = "";
            text = " LikeInComment***Id^^^CommentId^^^UserId^^^IsLike^^^ModifyDate^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.LikeInComments
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<LikeInComment>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.LikeInComments
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInComment>();
            }
            else
            {
                list = (from x in this.context.LikeInComments
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInComment>();
            }
            string result;
            if (list == null || list.Count<LikeInComment>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<LikeInComment>().ModifyDate,
					"***",
					list.LastOrDefault<LikeInComment>().ModifyDate,
					"***"
				});
                foreach (LikeInComment current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.ID,
						"^^^",
						current.CommentId,
						"^^^",
						current.UserId,
						"^^^",
						current.IsLike,
						"^^^",
						current.ModifyDate,
						"^^^",
						current.Date,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllLikeInCommentObject(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInCommentObject> list = null;
            string text = "";
            text = " LikeInCommentObject***Id^^^CommentId^^^UserId^^^IsLike^^^ModifyDate***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.LikeInCommentObjects
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<LikeInCommentObject>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.LikeInCommentObjects
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInCommentObject>();
            }
            else
            {
                list = (from x in this.context.LikeInCommentObjects
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInCommentObject>();
            }
            string result;
            if (list == null || list.Count<LikeInCommentObject>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<LikeInCommentObject>().ModifyDate,
					"***",
					list.LastOrDefault<LikeInCommentObject>().ModifyDate,
					"***"
				});
                foreach (LikeInCommentObject current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.CommentId,
						"^^^",
						current.UserId,
						"^^^",
						current.IsLike,
						"^^^",
						current.ModifyDate,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllLikeInFroum(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInFroum> list = null;
            string text = "";
            text = "LikeInFroum***Id^^^UserId^^^FroumId^^^Date^^^CommentId^^^Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.LikeInFroums
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<LikeInFroum>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.LikeInFroums
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInFroum>();
            }
            else
            {
                list = (from x in this.context.LikeInFroums
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInFroum>();
            }
            string result;
            if (list == null || list.Count<LikeInFroum>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<LikeInFroum>().ModifyDate,
					"***",
					list.LastOrDefault<LikeInFroum>().ModifyDate,
					"***"
				});
                foreach (LikeInFroum current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.FroumId,
						"^^^",
						current.Date,
						"^^^",
						current.CommentId,
						"^^^0***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllLikeInObject(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInObject> list = null;
            string text = "";
            text = " LikeInObject***Id^^^UserId^^^PaperId^^^Date^^^LikeType^^^Seen^^^IsLike^^^unFollow^^^unFollowDate***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.LikeInObjects
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<LikeInObject>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.LikeInObjects
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInObject>();
            }
            else
            {
                list = (from x in this.context.LikeInObjects
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInObject>();
            }
            string result;
            if (list == null || list.Count<LikeInObject>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<LikeInObject>().ModifyDate,
					"***",
					list.LastOrDefault<LikeInObject>().ModifyDate,
					"***"
				});
                foreach (LikeInObject current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.ObjectId,
						"^^^",
						current.Date,
						"^^^",
						current.LikeType,
						"^^^0",
                        current.IsLike,
                        "^^^",
                        current.unFollow,
                        "^^^",
                        current.unFollowDate,
                        "***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllLikeInPaper(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInPaper> list = null;
            string text = "";
            text = "LikeInPaper***Id^^^UserId^^^PaperId^^^Date^^^CommentId^^^Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.LikeInPapers
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<LikeInPaper>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.LikeInPapers
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInPaper>();
            }
            else
            {
                list = (from x in this.context.LikeInPapers
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<LikeInPaper>();
            }
            string result;
            if (list == null || list.Count<LikeInPaper>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<LikeInPaper>().ModifyDate,
					"***",
					list.LastOrDefault<LikeInPaper>().ModifyDate,
					"***"
				});
                foreach (LikeInPaper current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.PaperId,
						"^^^",
						current.Date,
						"^^^",
						current.CommentId,
						"^^^0***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllNews(string fromDate, string endDate, int isRefresh)
        {
            List<New> list = null;
            string text = "";
            text = " News***Id^^^Title^^^Description***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.News
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<New>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.News
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<New>();
            }
            else
            {
                list = (from x in this.context.News
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<New>();
            }
            string result;
            if (list == null || list.Count<New>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<New>().ModifyDate,
					"***",
					list.LastOrDefault<New>().ModifyDate,
					"***"
				});
                foreach (New current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Title,
						"^^^",
						current.Description,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllObject(string fromDate, string endDate, int isRefresh)
        {
            List<Object> list = null;
            string text = "";
            text = " Object***Id^^^Name^^^Phone^^^Email^^^Fax^^^Description^^^Cellphone^^^Address^^^Pdf1^^^Pdf2^^^Pdf3^^^Pdf4^^^ObjectTypeId^^^ObjectBrandTypeId^^^Facebook^^^Instagram^^^LinkedIn^^^Google^^^Site^^^Twitter^^^ParentId^^^rate^^^serverDate^^^MainObjectId^^^ObjectId^^^UserId^^^Date^^^IsActive^^^AgencyService***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Objects
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<Object>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Objects
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Object>();
            }
            else
            {
                list = (from x in this.context.Objects
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Object>();
            }
            string result;
            if (list == null || list.Count<Object>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Object>().ModifyDate,
					"***",
					list.LastOrDefault<Object>().ModifyDate,
					"***"
				});
                foreach (Object current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Name,
						"^^^",
						current.Phone,
						"^^^",
						current.Email,
						"^^^",
						current.Fax,
						"^^^",
						current.Description,
						"^^^",
						current.Cellphone,
						"^^^",
						current.Address,
						"^^^",
						current.Pdf1,
						"^^^",
						current.Pdf2,
						"^^^",
						current.Pdf3,
						"^^^",
						current.Pdf4,
						"^^^",
						current.ObjectTypeId,
						"^^^",
						current.ObjectBrandTypeId,
						"^^^",
						current.Facebook,
						"^^^",
						current.Instagram,
						"^^^",
						current.LinkedIn,
						"^^^",
						current.Google,
						"^^^",
						current.Site,
						"^^^",
						current.Twitter,
						"^^^",
						current.ParentId,
						"^^^",
						current.rate,
						"^^^",
						current.Date,
						"^^^",
						current.MainObjectId,
						"^^^",
						current.ObjectId,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"^^^",
						current.IsActive,
						"^^^",
						current.AgencyService,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllObjectInCity(string fromDate, string endDate, int isRefresh)
        {
            List<ObjectInCity> list = null;
            string text = "";
            text = " ObjectInCity***Id^^^ObjectId^^^CityId^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.ObjectInCities
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).ToList<ObjectInCity>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.ObjectInCities
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).ToList<ObjectInCity>();
            }
            else
            {
                list = (from x in this.context.ObjectInCities
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).ToList<ObjectInCity>();
            }
            string result;
            if (list == null || list.Count<ObjectInCity>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<ObjectInCity>().ModifyDate,
					"***",
					list.LastOrDefault<ObjectInCity>().ModifyDate,
					"***"
				});
                foreach (ObjectInCity current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.ObjectId,
						"^^^",
						current.CityId,
						"^^^",
						current.ModifyDate,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllPaper(string fromDate, string endDate, int isRefresh)
        {
            List<Paper> list = null;
            string text = "";
            text = " Paper***Id^^^Title^^^Context^^^UserId^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Papers
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<Paper>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Papers
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Paper>();
            }
            else
            {
                list = (from x in this.context.Papers
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Paper>();
            }
            string result;
            if (list == null || list.Count<Paper>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Paper>().ModifyDate,
					"***",
					list.LastOrDefault<Paper>().ModifyDate,
					"***"
				});
                foreach (Paper current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Title,
						"^^^",
						current.Context,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllTicket(string fromDate, string endDate, int isRefresh)
        {
            List<Ticket> list = null;
            string text = "";
            text = " Ticket***Id^^^Title^^^Desc^^^UserId^^^ Date^^^ TypeId^^^Name^^^Email^^^Mobile^^^Phone^^^Fax^^^ProvinceId^^^UName^^^UEmail^^^UPhonnumber^^^UFax^^^UAdress^^^UMobile^^^Seen^^^Day***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Tickets
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<Ticket>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Tickets
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Ticket>();
            }
            else
            {
                list = (from x in this.context.Tickets
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Ticket>();
            }
            string result;
            if (list == null || list.Count<Ticket>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Ticket>().ModifyDate,
					"***",
					list.LastOrDefault<Ticket>().ModifyDate,
					"***"
				});
                foreach (Ticket current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Title,
						"^^^",
						current.Desc,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"^^^",
						current.TypeId,
						"^^^",
						current.Name,
						"^^^",
						current.Email,
						"^^^",
						current.Mobile,
						"^^^",
						current.Phone,
						"^^^",
						current.Fax,
						"^^^",
						current.ProvinceId,
						" ^^^",
						current.UName,
						"^^^",
						current.UEmail,
						"^^^",
						current.UPhonnumber,
						"^^^",
						current.UFax,
						"^^^",
						current.UAdress,
						"^^^",
						current.UMobile,
						"^^^0^^^" + current.Day + "***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllUpdateDetails(string fromDate, string endDate, int isRefresh)
        {
            string[] array = fromDate.Split(new char[]
			{
				'-'
			});
            string[] array2 = endDate.Split(new char[]
			{
				'-'
			});
            string text = "";
            string text2 = this.getAllCmtInPaper(array[0], array2[0], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllCommentInFroum(array[1], array2[1], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllCommentInObject(array[2], array2[2], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllLikeInFroum(array[3], array2[3], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllLikeInObject(array[4], array2[4], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllLikeInPaper(array[5], array2[5], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllLikeInComment(array[6], array2[6], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllLikeInCommentObject(array[7], array2[7], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllObjectInCity(array[8], array2[8], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllCommentInPost(array[1], array2[1], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllLikeInPost(array[1], array2[1], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllLikeInCommentPost(array[1], array2[1], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            return text;
        }

        [WebMethod]
        public string getAllUpdateMaster(string fromDate, string endDate, int isRefresh)
        {
            string[] array = fromDate.Split(new char[]
			{
				'-'
			});
            string[] array2 = endDate.Split(new char[]
			{
				'-'
			});
            string text = "";
            string text2 = this.getAllAnad(array[0], array2[0], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllFroum(array[1], array2[1], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllNews(array[2], array2[2], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllObject(array[3], array2[3], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllPaper(array[4], array2[4], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllTicket(array[5], array2[5], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllUser(array[6], array2[6], isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllObjectInCity("", "", isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }
            text2 = this.getAllPost("", "", isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }

            text2 = this.getAllSubAdmin("", "", isRefresh);
            if (text2 != "")
            {
                text = text + text2 + "&&&";
            }


            return text;
        }

        [WebMethod]
        public string getAllUser(string fromDate, string endDate, int isRefresh)
        {
            List<User> list = null;
            string text = "";
            text = "Users***Id^^^Name^^^Email^^^Password^^^Phonenumber^^^mobailenumber^^^Faxnumber^^^Address^^^Date^^^ShowInfoItem^^^BirthDay^^^CityId***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Users
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<User>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Users
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<User>();
            }
            else
            {
                list = (from x in this.context.Users
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<User>();
            }
            string result;
            if (list == null || list.Count<User>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<User>().ModifyDate,
					"***",
					list.LastOrDefault<User>().ModifyDate,
					"***"
				});
                foreach (User current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Name,
						"^^^",
						current.Email,
						"^^^",
						current.Password,
						"^^^",
						current.Phonenumber,
						"^^^",
						current.Mobailenumber,
						"^^^",
						current.Faxnumber,
						"^^^",
						current.Address,
						"^^^",
						current.Date,
						"^^^",
						current.ShowInfoItem,
						"^^^",
						current.BirthDay,
						"^^^",
						current.CityId,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllPost(string fromDate, string endDate, int isRefresh)
        {
            List<Post> list = null;
            string text = "";
            text = "Post***Id^^^UserId^^^objectId^^^Description^^^Seen^^^Submit^^^Date^^^seenBefore***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Posts
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<Post>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Posts
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Post>();
            }
            else
            {
                list = (from x in this.context.Posts
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Post>();
            }
            string result;
            if (list == null || list.Count<Post>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Post>().ModifyDate,
					"***",
					list.LastOrDefault<Post>().ModifyDate,
					"***"
				});
                foreach (Post current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.ObjectId,
						"^^^",
						current.Description,
						"^^^",
						current.Seen,
						"^^^",
						current.Submit,
						"^^^",
						current.Date,
						"^^^",
						current.seenBefore,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllSubAdmin(string fromDate, string endDate, int isRefresh)
        {
            List<SubAdmin> list = null;
            string text = "";
            text = "SubAdmin***Id^^^UserId^^^objectId^^^AdminId^^^Date^^^ModifyDate***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.SubAdmins
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<SubAdmin>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.SubAdmins
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<SubAdmin>();
            }
            else
            {
                list = (from x in this.context.SubAdmins
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<SubAdmin>();
            }
            string result;
            if (list == null || list.Count<SubAdmin>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<SubAdmin>().ModifyDate,
					"***",
					list.LastOrDefault<SubAdmin>().ModifyDate,
					"***"
				});
                foreach (SubAdmin current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.ObjectId,
						"^^^",
						current.AdminId,
						"^^^",
						current.Date,
						"^^^",
						current.ModifyDate,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllSubAdminByObjectId(int objectId)
        {
            List<SubAdmin> list = null;
            string text = "";
            text = "SubAdmin***Id^^^UserId^^^objectId^^^AdminId^^^Date^^^ModifyDate***";
            string currentDate = this.getServerDateMilis();
           
            list = (from x in this.context.SubAdmins
                    where x.ObjectId == objectId
                    orderby x.ModifyDate descending
                    select x).ToList<SubAdmin>();
            
            string result;
            if (list == null || list.Count<SubAdmin>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<SubAdmin>().ModifyDate,
					"***",
					list.LastOrDefault<SubAdmin>().ModifyDate,
					"***"
				});
                foreach (SubAdmin current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.ObjectId,
						"^^^",
						current.AdminId,
						"^^^",
						current.Date,
						"^^^",
						current.ModifyDate,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        public string getAllPostByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Post> list = null;
            string text = "";
            text = "Post***Id^^^UserId^^^Description^^^Seen^^^Submit^^^Date^^^seenBefore***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Posts
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0 && x.UserId == userId
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<Post>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Posts
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == userId
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Post>();
            }
            else
            {
                list = (from x in this.context.Posts
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == userId
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Post>();
            }
            string result;
            if (list == null || list.Count<Post>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Post>().ModifyDate,
					"***",
					list.LastOrDefault<Post>().ModifyDate,
					"***"
				});
                foreach (Post current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.Description,
						"^^^",
						current.Seen,
						"^^^",
						current.Submit,
						"^^^",
						current.Date,
						"^^^",
						current.seenBefore,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllPostByObjectId(string fromDate, string endDate, int isRefresh, int objectId)
        {
            List<Post> list = null;
            string text = "";
            text = "Post***Id^^^UserId^^^Description^^^Seen^^^Submit^^^Date^^^seenBefore^^^objectId***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Posts
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0 && x.ObjectId == (int?)objectId
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<Post>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Posts
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0 && x.ObjectId == (int?)objectId
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Post>();
            }
            else
            {
                list = (from x in this.context.Posts
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0 && x.ObjectId == (int?)objectId
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Post>();
            }
            string result;
            if (list == null || list.Count<Post>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Post>().ModifyDate,
					"***",
					list.LastOrDefault<Post>().ModifyDate,
					"***"
				});
                foreach (Post current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.Description,
						"^^^",
						current.Seen,
						"^^^",
						current.Submit,
						"^^^",
						current.Date,
						"^^^",
						current.seenBefore,
						"^^^",
						current.ObjectId,
						"^^^",
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllPostByUserIdAndObjectId(string fromDate, string endDate, int isRefresh, int userId, int objectId)
        {
            List<Post> list = null;
            string text = "";
            text = "Post***Id^^^UserId^^^Description^^^Seen^^^Submit^^^Date^^^seenBefore***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Posts
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0 && x.ObjectId == (int?)objectId && x.UserId == userId
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<Post>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Posts
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0 && x.ObjectId == (int?)objectId && x.UserId == userId
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Post>();
            }
            else
            {
                list = (from x in this.context.Posts
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0 && x.ObjectId == (int?)objectId && x.UserId == userId
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Post>();
            }
            string result;
            if (list == null || list.Count<Post>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Post>().ModifyDate,
					"***",
					list.LastOrDefault<Post>().ModifyDate,
					"***"
				});
                foreach (Post current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.Description,
						"^^^",
						current.Seen,
						"^^^",
						current.Submit,
						"^^^",
						current.Date,
						"^^^",
						current.seenBefore,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        private DateTime getCuurentDate()
        {
            return DateTime.Now;
        }

        [WebMethod]
        public string getCuurentServerDate()
        {
            return DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString();
        }

        [WebMethod]
        public byte[] getAnadImage(string tableName, int id, string fromDate)
        {
            byte[] array = null;
            byte[] result;
            if (fromDate == null || "" == fromDate)
            {
                Anad anad = (from x in this.context.Anads
                             where x.Id == id
                             select x).FirstOrDefault<Anad>();
                if (anad != null && anad.Image != null)
                {
                    array = anad.Image.ToArray();
                }
                result = array;
            }
            else
            {
                Anad anad = (from x in this.context.Anads
                             where x.ImageServerDate != null && fromDate.CompareTo(x.ImageServerDate) < 0 && x.Id == id
                             select x).FirstOrDefault<Anad>();
                if (anad != null && anad.Image != null)
                {
                    array = anad.Image.ToArray();
                }
                result = array;
            }
            return result;
        }

        [WebMethod]
        public byte[] getPostImage(string tableName, int id, string fromDate)
        {
            byte[] result = null;
            if (fromDate == null || "".Equals(fromDate))
            {
                Post post = (from x in this.context.Posts
                             where x.Id == id
                             select x).FirstOrDefault<Post>();
                if (post != null && post.Photo != null)
                {
                    result = post.Photo.ToArray();
                }
            }
            else
            {
                Post post = (from x in this.context.Posts
                             where x.Id == id && fromDate.CompareTo(x.ImageServerDate) < 0
                             select x).FirstOrDefault<Post>();
                if (post != null && post.Photo != null)
                {
                    result = post.Photo.ToArray();
                }
            }
            return result;
        }

        [WebMethod]
        public byte[] getObject1Image(string tableName, int id, string fromDate)
        {
            byte[] result = null;
            if (fromDate == null || "".Equals(fromDate))
            {
                Object @object = (from x in this.context.Objects
                                  where x.Id == id
                                  select x).FirstOrDefault<Object>();
                if (@object != null && @object.Image1 != null)
                {
                    result = @object.Image1.ToArray();
                }
            }
            else
            {
                Object @object = (from x in this.context.Objects
                                  where x.Id == id && x.Image1Date != null && fromDate.CompareTo(x.Image1Date) > 0
                                  select x).FirstOrDefault<Object>();
                if (@object != null && @object.Image1 != null)
                {
                    result = @object.Image1.ToArray();
                }
            }
            return result;
        }

        [WebMethod]
        public byte[] getObject2Image(string tableName, int id, string fromDate)
        {
            byte[] result = null;
            if (fromDate == null || "".Equals(fromDate))
            {
                Object @object = (from x in this.context.Objects
                                  where x.Id == id
                                  select x).FirstOrDefault<Object>();
                if (@object != null && @object.Image2 != null)
                {
                    result = @object.Image2.ToArray();
                }
            }
            else
            {
                Object @object = (from x in this.context.Objects
                                  where x.Id == id && x.Image2Date != null && fromDate.CompareTo(x.Image2Date) > 0
                                  select x).FirstOrDefault<Object>();
                if (@object != null && @object.Image2 != null)
                {
                    result = @object.Image2.ToArray();
                }
            }
            return result;
        }

        [WebMethod]
        public byte[] getObject3Image(string tableName, int id, string fromDate)
        {
            byte[] result = null;
            if (fromDate == null || "".Equals(fromDate))
            {
                Object @object = (from x in this.context.Objects
                                  where x.Id == id
                                  select x).FirstOrDefault<Object>();
                if (@object != null && @object.Image3 != null)
                {
                    result = @object.Image3.ToArray();
                }
            }
            else
            {
                Object @object = (from x in this.context.Objects
                                  where x.Id == id && x.Image3Date != null && fromDate.CompareTo(x.Image3Date) > 0
                                  select x).FirstOrDefault<Object>();
                if (@object != null && @object.Image3 != null)
                {
                    result = @object.Image3.ToArray();
                }
            }
            return result;
        }

        [WebMethod]
        public string getServerDateMilis()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        [WebMethod]
        public string getUserById(string Id)
        {
            string text = "";
            text = "Users***Id^^^Name^^^Email^^^Password^^^Phonenumber^^^mobailenumber^^^Faxnumber^^^Address^^^Date^^^ShowInfoItem^^^BirthDay^^^CityId***";
            string[] tempParams = Id.Split(new char[]
			{
				'-'
			});
            List<User> list = (from x in this.context.Users
                               where tempParams.Contains(x.Id.ToString())
                               select x).ToList<User>();
            if (list == null || list.Count<User>() <= 0)
            {
                text = "";
            }
            else
            {
                text += "******";
                foreach (User current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Name,
						"^^^",
						current.Email,
						"^^^",
						current.Password,
						"^^^",
						current.Phonenumber,
						"^^^",
						current.Mobailenumber,
						"^^^",
						current.Faxnumber,
						"^^^",
						current.Address,
						"^^^",
						current.Date,
						"^^^",
						current.ShowInfoItem,
						"^^^",
						current.BirthDay,
						"^^^",
						current.CityId,
						"***"
					});
                }
            }
            return text + "&&&";
        }

        [WebMethod]
        public byte[] getUsersImage(int id, string fromDate)
        {
            byte[] array = null;
            byte[] result;
            if (fromDate == null || "" == fromDate)
            {
                User user = (from x in this.context.Users
                             where x.Id == id
                             select x).FirstOrDefault<User>();
                if (user != null && user.Image != null)
                {
                    array = user.Image.ToArray();
                }
                result = array;
            }
            else
            {
                User user = (from x in this.context.Users
                             where x.ImageServerDate != null && fromDate.CompareTo(x.ImageServerDate) < 0 && x.Id == id
                             select x).FirstOrDefault<User>();
                if (user != null && user.Image != null)
                {
                    array = user.Image.ToArray();
                }
                result = array;
            }
            return result;
        }

        [WebMethod]
        public byte[] getTicketImage(int id, string fromDate)
        {
            byte[] array = null;
            byte[] result;
            if (fromDate == null || "" == fromDate)
            {
                Ticket ticket = (from x in this.context.Tickets
                                 where x.Id == id
                                 select x).FirstOrDefault<Ticket>();
                if (ticket != null)
                {
                    array = ticket.Image.ToArray();
                }
                result = array;
            }
            else
            {
                Ticket ticket = (from x in this.context.Tickets
                                 where x.ImageServerDate != null && fromDate.CompareTo(x.ImageServerDate) < 0 && x.Id == id
                                 select x).FirstOrDefault<Ticket>();
                if (ticket != null)
                {
                    array = ticket.Image.ToArray();
                }
                result = array;
            }
            return result;
        }

        [WebMethod]
        public string login(string phone, string password)
        {
            string text = "";
            IQueryable<User> queryable = from x in this.context.Users
                                         where phone.Equals(x.Mobailenumber) && password.Equals(x.Password)
                                         select x;
            if (queryable != null)
            {
                User user = queryable.FirstOrDefault<User>();
                if (user != null)
                {
                    text = "Users***Id^^^Name^^^Email^^^Password^^^Phonenumber^^^mobailenumber^^^Faxnumber^^^Address^^^Date^^^ShowInfoItem^^^BirthDay^^^CityId***";
                    object obj = text + "******";
                    text = string.Concat(new object[]
					{
						obj,
						user.Id,
						"^^^",
						user.Name,
						"^^^",
						user.Email,
						"^^^",
						user.Password,
						"^^^",
						user.Phonenumber,
						"^^^",
						user.Mobailenumber,
						"^^^",
						user.Faxnumber,
						"^^^",
						user.Address,
						"^^^",
						user.Date,
						"^^^",
						user.ShowInfoItem,
						"^^^",
						user.BirthDay,
						"^^^",
						user.CityId,
						"***&&&"
					});
                }
            }
            return text;
        }

        [WebMethod]
        public bool loginViaFacebook()
        {
            return false;
        }

        [WebMethod]
        public bool loginViaGoogle()
        {
            return false;
        }

        [WebMethod]
        public bool needUpdate(string tableName, int userId, string serverDate)
        {
            this.context.ExecuteCommand("SELECT count(*) FROM " + tableName + " WHERE ", new object[0]);
            return false;
        }

        [WebMethod]
        public string getObject1ImageDate(string Id)
        {
            Object @object = (from c in this.context.Objects
                              where c.Id == Convert.ToInt32(Id)
                              select c).FirstOrDefault<Object>();
            string result = "";
            if (@object != null && @object.Image1Date != null)
            {
                result = @object.Image1Date;
            }
            return result;
        }

        [WebMethod]
        public string getObject2ImageDate(string Id)
        {
            Object @object = (from c in this.context.Objects
                              where c.Id == Convert.ToInt32(Id)
                              select c).FirstOrDefault<Object>();
            string result = "";
            if (@object != null && @object.Image2Date != null)
            {
                result = @object.Image2Date;
            }
            return result;
        }

        [WebMethod]
        public string getObject3ImageDate(string Id)
        {
            Object @object = (from c in this.context.Objects
                              where c.Id == Convert.ToInt32(Id)
                              select c).FirstOrDefault<Object>();
            string result = "";
            if (@object != null && @object.Image3Date != null)
            {
                result = @object.Image3Date;
            }
            return result;
        }

        [WebMethod]
        public string getUserImageDate(string Id)
        {
            User user = (from c in this.context.Users
                         where c.Id == Convert.ToInt32(Id)
                         select c).FirstOrDefault<User>();
            string result = "";
            if (user != null && user.ImageServerDate != null)
            {
                result = user.ImageServerDate;
            }
            return result;
        }

        [WebMethod]
        public string getPostImageDate(string Id)
        {
            Post post = (from c in this.context.Posts
                         where c.Id == Convert.ToInt32(Id)
                         select c).FirstOrDefault<Post>();
            string result = "";
            if (post != null && post.ImageServerDate != null)
            {
                result = post.ImageServerDate;
            }
            return result;
        }

        [WebMethod]
        public string getAnadImageDate(string Id)
        {
            Anad anad = (from c in this.context.Anads
                         where c.Id == Convert.ToInt32(Id)
                         select c).FirstOrDefault<Anad>();
            return "";
        }

        [WebMethod]
        public string getTicketImageDate(string Id)
        {
            Ticket ticket = (from c in this.context.Tickets
                             where c.Id == Convert.ToInt32(Id)
                             select c).FirstOrDefault<Ticket>();
            string result = "";
            if (ticket != null && ticket.ImageServerDate != null)
            {
                result = ticket.ImageServerDate;
            }
            return result;
        }

        [WebMethod]
        public bool recoverPassword(string email)
        {
            bool result;
            try
            {
                string password = (from x in this.context.Users
                                   where x.Email == email
                                   select x).First<User>().Password;
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        [WebMethod]
        public string register(string username, string email, string password, string phone, string mobile, string fax, string address, string date, string modifyDate, string BirthDay, string cityId)
        {
            string result;
            if (this.context.Users.Any((User x) => x.Mobailenumber == mobile))
            {
                result = "";
            }
            else
            {
                try
                {
                    User user = new User
                    {
                        Name = username,
                        Email = email,
                        Password = password,
                        Phonenumber = phone,
                        Mobailenumber = mobile,
                        Faxnumber = fax,
                        Address = address,
                        Date = date,
                        ModifyDate = date,
                        BirthDay = BirthDay,
                        CityId = (cityId != null) ? new int?(Convert.ToInt32(cityId)) : new int?(0)
                    };
                    this.context.Users.InsertOnSubmit(user);
                    this.context.SubmitChanges();
                    result = user.Id.ToString();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            return result;
        }

        [WebMethod]
        public string regPhone(string phone)
        {
            string text = "";
            IQueryable<User> queryable = from x in this.context.Users
                                         where phone.Equals(x.Mobailenumber)
                                         select x;
            if (queryable != null)
            {
                User user = queryable.FirstOrDefault<User>();
                if (user != null)
                {
                    text = "Users***Id^^^Name^^^Email^^^Password^^^Phonenumber^^^mobailenumber^^^Faxnumber^^^Address^^^Date^^^ShowInfoItem^^^BirthDay^^^CityId***";
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						user.Id,
						"^^^",
						user.Name,
						"^^^",
						user.Email,
						"^^^",
						user.Password,
						"^^^",
						user.Phonenumber,
						"^^^",
						user.Mobailenumber,
						"^^^",
						user.Faxnumber,
						"^^^",
						user.Address,
						"^^^",
						user.Date,
						"^^^",
						user.ShowInfoItem,
						"^^^",
						user.BirthDay,
						"^^^",
						user.CityId,
						"***&&&"
					});
                }
            }
            return text;
        }

        [WebMethod]
        public string Saving(string tableName, string param, string IsUpdate, string Id)
        {
            bool flag = Convert.ToInt32(IsUpdate) > 0;
            string text = "";
            int num = 0;
            string text2 = "";
            string text3 = "";
            string[] array = param.Split(new char[]
			{
				'-'
			});
            string text4 = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null && array[i].Contains(":"))
                {
                    string str = array[i].Substring(0, array[i].IndexOf(":")) + ",";
                    string text5 = array[i].Substring(0, array[i].IndexOf(":"));
                    text4 = array[i].Substring(array[i].IndexOf(":") + 1, array[i].Length - array[i].IndexOf(":") - 1);
                    if (flag)
                    {
                        if (text4 != null && text4 != "")
                        {
                            string text6 = text;
                            text = string.Concat(new string[]
							{
								text6,
								text5,
								"='",
								text4,
								"',"
							});
                        }
                    }
                    else
                    {
                        text2 += str;
                        try
                        {
                            int num2 = Convert.ToInt32(text4);
                            text3 = text3 + text4 + ",";
                        }
                        catch (Exception var_13_186)
                        {
                            text3 = text3 + "'" + text4 + "',";
                        }
                    }
                }
            }
            if (text != null && text.Contains(","))
            {
                text = text.Remove(text.LastIndexOf(","), 1);
            }
            if (text2 != null && text2.Contains(","))
            {
                text2 = text2.Remove(text2.LastIndexOf(","), 1);
            }
            if (text3 != null && text3.Contains(","))
            {
                text3 = text3.Remove(text3.LastIndexOf(","), 1);
            }



            if (flag)
            {
                if (text == "")
                {
                    num = -1;
                }
                else
                {
                    int value = this.context.ExecuteQuery<int>(string.Concat(new string[]
					{
						"UPDATE ",
						tableName,
						" SET ",
						text,
						" WHERE Id=",
						Id
					}), new object[0]).FirstOrDefault<int>();
                    try
                    {
                        num = Convert.ToInt32(value);
                    }
                    catch (Exception var_15_2E0)
                    {
                        num = -1;
                    }
                }
            }
            else if (text3 == "")
            {
                num = -1;
            }
            else
            {
                string query = string.Concat(new string[]
				{
					"INSERT INTO ",
					tableName,
					" (",
					text2,
					") VALUES (",
					text3,
					") ; select scope_identity()"
				});
                decimal value2 = this.context.ExecuteQuery<decimal>(query, new object[0]).First<decimal>();
                try
                {
                    num = Convert.ToInt32(value2);
                }
                catch (Exception var_18_37B)
                {
                    num = -1;
                }
            }
            return num.ToString();
        }

        [WebMethod]
        public int savingImage(string tableName, string fieldName, string id, byte[] image)
        {
            return this.context.ExecuteCommand(string.Concat(new string[]
			{
				"Update [",
				tableName,
				"] SET ",
				fieldName,
				"={0} , ImageServerDate = '",
				this.getServerDateMilis(),
				"'  WHERE Id=",
				id
			}), new object[]
			{
				image
			});
        }

        [WebMethod]
        public string savingImage3(string tableName, string fieldName1, string fieldName2, string fieldName3, string id, byte[] image1, byte[] image2, byte[] image3)
        {
            string str = "Update [" + tableName + "] SET ";
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            object[] array = new object[3];
            string serverDateMilis = this.getServerDateMilis();
            if (image1 != null)
            {
                flag = true;
                str = str + fieldName1 + "={0},  Image1Date=" + serverDateMilis;
            }
            if (image2 != null)
            {
                flag2 = true;
                if (flag)
                {
                    str += " , ";
                }
                str = str + fieldName2 + "={1},  Image2Date=" + serverDateMilis;
            }
            if (image3 != null)
            {
                if (flag2 || flag)
                {
                    str += " , ";
                }
                flag3 = true;
                str = str + fieldName3 + "={2},  Image3Date=" + serverDateMilis;
            }
            if (flag)
            {
                array[0] = image1;
            }
            if (flag2)
            {
                array[1] = image2;
            }
            if (flag3)
            {
                array[2] = image3;
            }
            int num = this.context.ExecuteCommand(str + " WHERE Id=" + id, array);
            return serverDateMilis;
        }

        [WebMethod]
        public string getAllTicketByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Ticket> list = null;
            string text = "";
            text = " Ticket***Id^^^Title^^^Desc^^^UserId^^^ Date^^^ TypeId^^^Name^^^Email^^^Mobile^^^Phone^^^Fax^^^ProvinceId^^^UName^^^UEmail^^^UPhonnumber^^^UFax^^^UAdress^^^UMobile^^^Seen^^^Day***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Tickets
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0 && x.UserId == (int?)userId
                            orderby x.ModifyDate descending
                            select x).ToList<Ticket>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Tickets
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).ToList<Ticket>();
            }
            else
            {
                list = (from x in this.context.Tickets
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).ToList<Ticket>();
            }
            string result;
            if (list == null || list.Count<Ticket>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Ticket>().ModifyDate,
					"***",
					list.LastOrDefault<Ticket>().ModifyDate,
					"***"
				});
                foreach (Ticket current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Title,
						"^^^",
						current.Desc,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"^^^",
						current.TypeId,
						"^^^",
						current.Name,
						"^^^",
						current.Email,
						"^^^",
						current.Mobile,
						"^^^",
						current.Phone,
						"^^^",
						current.Fax,
						"^^^",
						current.ProvinceId,
						" ^^^",
						current.UName,
						"^^^",
						current.UEmail,
						"^^^",
						current.UPhonnumber,
						"^^^",
						current.UFax,
						"^^^",
						current.UAdress,
						"^^^",
						current.UMobile,
						"^^^0^^^" + current.Day + "***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllObjectByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Object> list = null;
            string text = "";
            text = " Object***Id^^^Name^^^Phone^^^Email^^^Fax^^^Description^^^Cellphone^^^Address^^^Pdf1^^^Pdf2^^^Pdf3^^^Pdf4^^^ObjectTypeId^^^ObjectBrandTypeId^^^Facebook^^^Instagram^^^LinkedIn^^^Google^^^Site^^^Twitter^^^ParentId^^^rate^^^serverDate^^^MainObjectId^^^ObjectId^^^UserId^^^Date^^^IsActive^^^AgencyService***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Objects
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0 && x.UserId == (int?)userId
                            orderby x.ModifyDate descending
                            select x).ToList<Object>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Objects
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).ToList<Object>();
            }
            else
            {
                list = (from x in this.context.Objects
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).ToList<Object>();
            }
            string result;
            if (list == null || list.Count<Object>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Object>().ModifyDate,
					"***",
					list.LastOrDefault<Object>().ModifyDate,
					"***"
				});
                foreach (Object current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Name,
						"^^^",
						current.Phone,
						"^^^",
						current.Email,
						"^^^",
						current.Fax,
						"^^^",
						current.Description,
						"^^^",
						current.Cellphone,
						"^^^",
						current.Address,
						"^^^",
						current.Pdf1,
						"^^^",
						current.Pdf2,
						"^^^",
						current.Pdf3,
						"^^^",
						current.Pdf4,
						"^^^",
						current.ObjectTypeId,
						"^^^",
						current.ObjectBrandTypeId,
						"^^^",
						current.Facebook,
						"^^^",
						current.Instagram,
						"^^^",
						current.LinkedIn,
						"^^^",
						current.Google,
						"^^^",
						current.Site,
						"^^^",
						current.Twitter,
						"^^^",
						current.ParentId,
						"^^^",
						current.rate,
						"^^^",
						current.Date,
						"^^^",
						current.MainObjectId,
						"^^^",
						current.ObjectId,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"^^^",
						current.IsActive,
						"^^^",
						current.AgencyService,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllFroumByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Froum> list = null;
            string text = "";
            text = " Froum***Id^^^Title^^^Description^^^UserId^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Froums
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0 && x.UserId == (int?)userId
                            orderby x.ModifyDate descending
                            select x).ToList<Froum>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Froums
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).ToList<Froum>();
            }
            else
            {
                list = (from x in this.context.Froums
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).ToList<Froum>();
            }
            string result;
            if (list == null || list.Count<Froum>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Froum>().ModifyDate,
					"***",
					list.LastOrDefault<Froum>().ModifyDate,
					"***"
				});
                foreach (Froum current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Title,
						"^^^",
						current.Description,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllPaperByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Paper> list = null;
            string text = "";
            text = " Paper***Id^^^Title^^^Context^^^UserId^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Papers
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0 && x.UserId == (int?)userId
                            orderby x.ModifyDate descending
                            select x).ToList<Paper>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Papers
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).ToList<Paper>();
            }
            else
            {
                list = (from x in this.context.Papers
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).ToList<Paper>();
            }
            string result;
            if (list == null || list.Count<Paper>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Paper>().ModifyDate,
					"***",
					list.LastOrDefault<Paper>().ModifyDate,
					"***"
				});
                foreach (Paper current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Title,
						"^^^",
						current.Context,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllObjectLikedByUserId(int userId)
        {
            string text = " Object***Id^^^Name^^^Phone^^^Email^^^Fax^^^Description^^^Cellphone^^^Address^^^Pdf1^^^Pdf2^^^Pdf3^^^Pdf4^^^ObjectTypeId^^^ObjectBrandTypeId^^^Facebook^^^Instagram^^^LinkedIn^^^Google^^^Site^^^Twitter^^^ParentId^^^rate^^^serverDate^^^MainObjectId^^^ObjectId^^^UserId^^^Date^^^IsActive^^^AgencyService***";
            string serverDateMilis = this.getServerDateMilis();
            object obj;
            List<Object> list = (from obj2 in this.context.Objects
                                 join lio in this.context.LikeInObjects on (int?)obj2.Id equals lio.ObjectId
                                 where lio.UserId == userId
                                 select obj2).ToList<Object>();
            text = string.Concat(new string[]
			{
				text,
				list.FirstOrDefault<Object>().ModifyDate,
				"***",
				list.LastOrDefault<Object>().ModifyDate,
				"***"
			});
            foreach (Object current in list)
            {
                obj = text;
                text = string.Concat(new object[]
				{
					obj,
					current.Id,
					"^^^",
					current.Name,
					"^^^",
					current.Phone,
					"^^^",
					current.Email,
					"^^^",
					current.Fax,
					"^^^",
					current.Description,
					"^^^",
					current.Cellphone,
					"^^^",
					current.Address,
					"^^^",
					current.Pdf1,
					"^^^",
					current.Pdf2,
					"^^^",
					current.Pdf3,
					"^^^",
					current.Pdf4,
					"^^^",
					current.ObjectTypeId,
					"^^^",
					current.ObjectBrandTypeId,
					"^^^",
					current.Facebook,
					"^^^",
					current.Instagram,
					"^^^",
					current.LinkedIn,
					"^^^",
					current.Google,
					"^^^",
					current.Site,
					"^^^",
					current.Twitter,
					"^^^",
					current.ParentId,
					"^^^",
					current.rate,
					"^^^",
					current.Date,
					"^^^",
					current.MainObjectId,
					"^^^",
					current.ObjectId,
					"^^^",
					current.UserId,
					"^^^",
					current.Date,
					"^^^",
					current.IsActive,
					"^^^",
					current.AgencyService,
					"***"
				});
            }
            return text;
        }


        [WebMethod]
        public string getAllLikeInObjectByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<LikeInObject> list = null;
            string text = "";
            text = " LikeInObject***Id^^^UserId^^^PaperId^^^Date^^^LikeType^^^Seen^^^unFollow^^^unFollowDate***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.LikeInObjects
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0 && x.UserId == (int?)userId
                            orderby x.ModifyDate descending
                            select x).ToList<LikeInObject>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.LikeInObjects
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).ToList<LikeInObject>();
            }
            else
            {
                list = (from x in this.context.LikeInObjects
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).ToList<LikeInObject>();
            }
            string result;
            if (list == null || list.Count<LikeInObject>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<LikeInObject>().ModifyDate,
					"***",
					list.LastOrDefault<LikeInObject>().ModifyDate,
					"***"
				});
                foreach (LikeInObject current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.ObjectId,
						"^^^",
						current.Date,
						"^^^",
						current.LikeType,
						"^^^0",
                        current.unFollow,
                        "^^^",
                        current.unFollowDate
                        ,"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getAllObjectForUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            string text = " Object***Id^^^Name^^^Phone^^^Email^^^Fax^^^Description^^^Cellphone^^^Address^^^Pdf1^^^Pdf2^^^Pdf3^^^Pdf4^^^ObjectTypeId^^^ObjectBrandTypeId^^^Facebook^^^Instagram^^^LinkedIn^^^Google^^^Site^^^Twitter^^^ParentId^^^rate^^^serverDate^^^MainObjectId^^^ObjectId^^^UserId^^^Date^^^IsActive^^^AgencyService***";
            string serverDateMilis = this.getServerDateMilis();
            object obj;
            List<Object> list = (from obj2 in this.context.Objects
                                 join lio in this.context.LikeInObjects on (int?)obj2.Id equals lio.ObjectId
                                 join usr in this.context.Users on lio.UserId equals (int?)usr.Id
                                 where usr.Id == userId
                                 select obj2).ToList<Object>();
            text = string.Concat(new string[]
			{
				text,
				list.FirstOrDefault<Object>().ModifyDate,
				"***",
				list.LastOrDefault<Object>().ModifyDate,
				"***"
			});
            foreach (Object current in list)
            {
                obj = text;
                text = string.Concat(new object[]
				{
					obj,
					current.Id,
					"^^^",
					current.Name,
					"^^^",
					current.Phone,
					"^^^",
					current.Email,
					"^^^",
					current.Fax,
					"^^^",
					current.Description,
					"^^^",
					current.Cellphone,
					"^^^",
					current.Address,
					"^^^",
					current.Pdf1,
					"^^^",
					current.Pdf2,
					"^^^",
					current.Pdf3,
					"^^^",
					current.Pdf4,
					"^^^",
					current.ObjectTypeId,
					"^^^",
					current.ObjectBrandTypeId,
					"^^^",
					current.Facebook,
					"^^^",
					current.Instagram,
					"^^^",
					current.LinkedIn,
					"^^^",
					current.Google,
					"^^^",
					current.Site,
					"^^^",
					current.Twitter,
					"^^^",
					current.ParentId,
					"^^^",
					current.rate,
					"^^^",
					current.Date,
					"^^^",
					current.MainObjectId,
					"^^^",
					current.ObjectId,
					"^^^",
					current.UserId,
					"^^^",
					current.Date,
					"^^^",
					current.IsActive,
					"^^^",
					current.AgencyService,
					"***"
				});
            }
            return text;
        }

        [WebMethod]
        public string getAllAnadByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Anad> list = null;
            string text = "";
            text = " Anad***Id^^^UserId^^^ObjectId^^^Date^^^ TypeId^^^ ProvinceId^^^ Seen^^^UserId***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if (fromDate != null && fromDate != "")
                {
                    list = (from x in this.context.Anads
                            where x.ModifyDate != null && fromDate.CompareTo(x.ModifyDate) < 0 && x.UserId == (int?)userId
                            orderby x.ModifyDate descending
                            select x).Take(this.recordCount).ToList<Anad>();
                }
            }
            else if (endDate != null && endDate != "")
            {
                list = (from x in this.context.Anads
                        where x.ModifyDate != null && endDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Anad>();
            }
            else
            {
                list = (from x in this.context.Anads
                        where x.ModifyDate != null && currentDate.CompareTo(x.ModifyDate) >= 0 && x.UserId == (int?)userId
                        orderby x.ModifyDate descending
                        select x).Take(this.recordCount).ToList<Anad>();
            }
            string result;
            if (list == null || list.Count<Anad>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Anad>().ModifyDate,
					"***",
					list.LastOrDefault<Anad>().ModifyDate,
					"***"
				});
                foreach (Anad current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
                        current.UserId,
                        "^^^",
						current.ObjectId,
						"^^^",
						current.Date,
						"^^^",
						current.TypeId,
						"^^^",
						current.ProvinceId,
						"^^^0^^^",
						current.UserId,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public long getAllVisit(int objectId, int typeId)
        {
            return (from x in this.context.Visits
                    where x.ObjectId == (int?)objectId && x.TypeId == (int?)typeId
                    select x.UserId).LongCount<int?>();
        }

        [WebMethod]
        public string getObjectByObjectId(int objectId, string fromDate, string toDate, int provinceId = 0, int cityId = 0, int agencyService = 0)
        {
            string text = " Object***Id^^^Name^^^Phone^^^Email^^^Fax^^^Description^^^Cellphone^^^Address^^^Pdf1^^^Pdf2^^^Pdf3^^^Pdf4^^^ObjectTypeId^^^ObjectBrandTypeId^^^Facebook^^^Instagram^^^LinkedIn^^^Google^^^Site^^^Twitter^^^ParentId^^^rate^^^serverDate^^^MainObjectId^^^ObjectId^^^UserId^^^Date^^^IsActive^^^AgencyService***";
            object obj;
            List<Object> list = (from obj2 in this.context.Objects
                                 join oic in this.context.ObjectInCities on (int?)obj2.Id equals oic.ObjectId
                                 where obj2.ModifyDate != null && fromDate.CompareTo(obj2.ModifyDate) < 0 && toDate.CompareTo(obj2.ModifyDate) >= 0 && oic.CityId == (int?)cityId && obj2.AgencyService == (int?)agencyService
                                 orderby obj2.ModifyDate descending
                                 select obj2).ToList<Object>();
            string result;
            if (list.Count > 0)
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<Object>().ModifyDate,
					"***",
					list.LastOrDefault<Object>().ModifyDate,
					"***"
				});
                foreach (Object current in list)
                {
                    obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Name,
						"^^^",
						current.Phone,
						"^^^",
						current.Email,
						"^^^",
						current.Fax,
						"^^^",
						current.Description,
						"^^^",
						current.Cellphone,
						"^^^",
						current.Address,
						"^^^",
						current.Pdf1,
						"^^^",
						current.Pdf2,
						"^^^",
						current.Pdf3,
						"^^^",
						current.Pdf4,
						"^^^",
						current.ObjectTypeId,
						"^^^",
						current.ObjectBrandTypeId,
						"^^^",
						current.Facebook,
						"^^^",
						current.Instagram,
						"^^^",
						current.LinkedIn,
						"^^^",
						current.Google,
						"^^^",
						current.Site,
						"^^^",
						current.Twitter,
						"^^^",
						current.ParentId,
						"^^^",
						current.rate,
						"^^^",
						current.Date,
						"^^^",
						current.MainObjectId,
						"^^^",
						current.ObjectId,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"^^^",
						current.IsActive,
						"^^^",
						current.AgencyService,
						"***"
					});
                }
                result = text;
            }
            else
            {
                result = "";
            }
            return result;
        }

        [WebMethod]
        public string getPostCountByObjectId(int objectId)
        {
            return (from x in this.context.Posts
                    where x.ObjectId == (int?)objectId
                    select x).Count<Post>().ToString();
        }

        [WebMethod]
        public string getHappySadByObjectId(int objectId,int type)
        {
            return (from x in this.context.LikeInObjects
                    where x.ObjectId == (int?)objectId && x.LikeType == type
                    select x).Count<LikeInObject>().ToString();
        }

        [WebMethod]
        public string getUserByObjectId(int objectId,int type)
        {
            List<User> list = null;
            string text = "";
            text = "Users***Id^^^Name^^^Email^^^Password^^^Phonenumber^^^mobailenumber^^^Faxnumber^^^Address^^^Date^^^ShowInfoItem^^^BirthDay^^^CityId***";
            string currentDate = this.getServerDateMilis();
            list = (from x in this.context.Users
                    join lio in this.context.LikeInObjects on x.Id equals lio.UserId
                    where lio.ObjectId == objectId && lio.LikeType == type
                    orderby x.ModifyDate descending
                    select x).ToList<User>();
            string result;
            if (list == null || list.Count<User>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<User>().ModifyDate,
					"***",
					list.LastOrDefault<User>().ModifyDate,
					"***"
				});
                foreach (User current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Name,
						"^^^",
						current.Email,
						"^^^",
						current.Password,
						"^^^",
						current.Phonenumber,
						"^^^",
						current.Mobailenumber,
						"^^^",
						current.Faxnumber,
						"^^^",
						current.Address,
						"^^^",
						current.Date,
						"^^^",
						current.ShowInfoItem,
						"^^^",
						current.BirthDay,
						"^^^",
						current.CityId,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getLikeInObjectByObjectId(int objectId, int type)
        {
            List<LikeInObject> list = null;
            string text = "";
            text = " LikeInObject***Id^^^UserId^^^PaperId^^^Date^^^LikeType^^^Seen^^^unFollow^^^unFollowDate^^^IsLike***";
            string currentDate = this.getServerDateMilis();

            list = (from x in this.context.Users
                    join lio in this.context.LikeInObjects on x.Id equals lio.UserId
                    where lio.ObjectId == objectId && lio.LikeType == type
                    orderby x.ModifyDate descending
                    select lio).ToList<LikeInObject>();
            
            string result;
            if (list == null || list.Count<LikeInObject>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					list.FirstOrDefault<LikeInObject>().ModifyDate,
					"***",
					list.LastOrDefault<LikeInObject>().ModifyDate,
					"***"
				});
                foreach (LikeInObject current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.UserId,
						"^^^",
						current.ObjectId,
						"^^^",
						current.Date,
						"^^^",
						current.LikeType,
						"^^^0",
                        "^^^",
                        current.unFollow,
                        "^^^",
                        current.unFollowDate,
                        "^^^",
                        current.IsLike,
                        "***"
					});
                }
                result = text;
            }
            return result;
        }


        [WebMethod]
        public string getFollowerCountByObjectId(int objectId)
        {
            return (from x in this.context.LikeInObjects
                    where x.ObjectId == (int?)objectId && x.LikeType == (int?)0
                    select x).Count<LikeInObject>().ToString();
        }

        [WebMethod]
        public string getLikeInPostCount(int id)
        {
            return (from x in this.context.LikeInPosts
                    where x.PostId == (int?)id 
                    select x).Count<LikeInPost>().ToString();
        }

        [WebMethod]
        public string getLikeInPaperCount(int id)
        {
            return (from x in this.context.LikeInPapers
                    where x.PaperId == (int?)id
                    select x).Count<LikeInPaper>().ToString();
        }

        [WebMethod]
        public string getLikeInFroumCount(int id)
        {
            return (from x in this.context.LikeInFroums
                    where x.FroumId == (int?)id
                    select x).Count<LikeInFroum>().ToString();
        }

        [WebMethod]
        public string getCommentInPostCount(int id)
        {
            return (from x in this.context.CommentInPosts
                    where x.PostId == (int?)id
                    select x).Count<CommentInPost>().ToString();
        }

        [WebMethod]
        public string getCommentInPostById(int id)
        {
            List<CommentInPost> list = (from x in this.context.CommentInPosts
                    where x.PostId == (int?)id
                    select x).ToList();

            string text = " CommentInPost***Id^^^Description^^^PostId^^^UserId^^^Date^^^ CommentId***";
            string currentDate = this.getServerDateMilis();
            string result;
            if (list == null || list.Count<CommentInPost>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					"-1",
					"***",
					"-1",
					"***"
				});
                foreach (CommentInPost current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Description,
						"^^^",
						current.PostId,
						"^^^",
						current.UserId,
						"^^^",
						current.ModifyDate,
						"^^^",
						current.CommentId,
						"***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string getCommentInPaperCount(int id)
        {
            return (from x in this.context.CommentInPapers
                    where x.PaperId == (int?)id
                    select x).Count<CommentInPaper>().ToString();
        }

        [WebMethod]
        public string getCommentInPaperById(int id)
        {
            List<CommentInPaper> list = (from x in this.context.CommentInPapers
                    where x.PaperId == (int?)id
                    select x).ToList();

            string text = "";
            text = " CmtInPaper***Id^^^Desk^^^PaperId^^^UserId^^^Date^^^ CommentId^^^ Seen***";
            string currentDate = this.getServerDateMilis();
           
            string result;
            if (list == null || list.Count<CommentInPaper>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					"-1",
					"***",
					"-1",
					"***"
				});
                foreach (CommentInPaper current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.Id,
						"^^^",
						current.Desk,
						"^^^",
						current.PaperId,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"^^^",
						current.CommentId,
						"^^^0***"
					});
                }
                result = text;
            }
            return result;
        }


        [WebMethod]
        public string getCommentInFroumCount(int id)
        {
            return (from x in this.context.CommentInFroums
                    where x.FroumId == (int?)id
                    select x).Count<CommentInFroum>().ToString();
        }

        [WebMethod]
        public string getCommentInFroumById(int id)
        {
            List<CommentInFroum> list = null;
            list = (from x in this.context.CommentInFroums
                    where x.FroumId == (int?)id
                    select x).ToList();

            string text = "";
            text = " CommentInFroum***Id^^^Desk^^^FroumId^^^UserId^^^Date^^^CommentId^^^NumofLike^^^NumofDisLike^^^Seen***";
            string currentDate = this.getServerDateMilis();
            
            string result;
            if (list == null || list.Count<CommentInFroum>() <= 0)
            {
                result = "";
            }
            else
            {
                text = string.Concat(new string[]
				{
					text,
					"-1",
					"***",
					"-1",
					"***"
				});
                foreach (CommentInFroum current in list)
                {
                    object obj = text;
                    text = string.Concat(new object[]
					{
						obj,
						current.ID,
						"^^^",
						current.Desk,
						"^^^",
						current.FroumId,
						"^^^",
						current.UserId,
						"^^^",
						current.Date,
						"^^^",
						current.CommentId,
						"^^^",
						current.NumofLike,
						"^^^",
						current.NumofDisLike,
						"^^^0***"
					});
                }
                result = text;
            }
            return result;
        }

        [WebMethod]
        public string isObjectHasSubObject(int objectId, int agencyService)
        {
            return (from x in this.context.Objects
                    where x.ObjectId == (int?)objectId && x.AgencyService == agencyService
                    select x).Count<Object>().ToString();
        }


        [WebMethod]
        public string getSubObjectsInCity(int objectId, int agencyService,int cityId)
        {
            return (from x in this.context.Objects
                    join oInCity in context.ObjectInCities on x.Id equals oInCity.ObjectId
                    where x.ObjectId == (int?)objectId && x.AgencyService == agencyService && oInCity.CityId == cityId
                    select x).Count<Object>().ToString();
        }

        [WebMethod]
        public string getSubObjectsInProvince(int objectId, int agencyService, int provinceId)
        {
            return (from x in this.context.Objects
                    join oInProvince in context.ObjectInProvinces on x.Id equals oInProvince.ObjectId
                    where x.ObjectId == (int?)objectId && x.AgencyService == agencyService && oInProvince.ProvinceId == provinceId
                    select x).Count<Object>().ToString();
        }
    }
}
