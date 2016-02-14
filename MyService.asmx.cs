namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web.Services;

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
            string str = "";
            string str2 = "";
            string str3 = "";
            string[] strArray = param.Split(new char[] { '-' });
            string str4 = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                if ((strArray[i] != null) && strArray[i].Contains(":"))
                {
                    str = strArray[i].Substring(0, strArray[i].IndexOf(":"));
                    str4 = strArray[i].Substring(strArray[i].IndexOf(":") + 1, (strArray[i].Length - strArray[i].IndexOf(":")) - 1);
                    try
                    {
                        int num3 = Convert.ToInt32(str4);
                        str2 = str4;
                    }
                    catch (Exception)
                    {
                        str2 = "'" + str4 + "'";
                    }
                    string str6 = str3;
                    str3 = str6 + str + "=" + str2 + " AND ";
                }
            }
            str3 = str3.Remove(str3.LastIndexOf("AND"), 3);
         
            return this.context.ExecuteCommand("Delete From " + tableName + " WHERE " + str3, new object[0]).ToString();
        }

        [WebMethod]
        public string DeletingRecord(string tableName, string Id)
        {
            int num = this.context.ExecuteCommand("Delete From " + tableName + " WHERE Id =" + Id, new object[0]);
            num = this.context.ExecuteCommand("Delete From LikeIn" + tableName + " WHERE " + tableName + "Id =" + Id, new object[0]);
            return this.context.ExecuteCommand("Delete From CommentIn" + tableName + " WHERE " + tableName + "Id =" + Id, new object[0]).ToString();
        }

        [WebMethod]
        public string getAllAnad(string fromDate, string endDate, int isRefresh)
        {
            List<Anad> source = null;
            string str = "";
            str = " Anad***Id^^^ObjectId^^^Date^^^ TypeId^^^ ProvinceId^^^ Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Anads
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<Anad>(this.recordCount).ToList<Anad>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Anads
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Anad>(this.recordCount).ToList<Anad>();
            }
            else
            {
                source = (from x in this.context.Anads
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Anad>(this.recordCount).ToList<Anad>();
            }
            if ((source == null) || (source.Count<Anad>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Anad>().ModifyDate + "***") + source.LastOrDefault<Anad>().ModifyDate + "***";
            foreach (Anad anad in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, anad.Id, "^^^", anad.ObjectId, "^^^", anad.Date, "^^^", anad.TypeId, "^^^", anad.ProvinceId, "^^^0***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllCmtInPaper(string fromDate, string endDate, int isRefresh)
        {
            List<CommentInPaper> source = null;
            string str = "";
            str = " CmtInPaper***Id^^^Desk^^^PaperId^^^UserId^^^Date^^^ CommentId^^^ Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.CommentInPapers
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<CommentInPaper>(this.recordCount).ToList<CommentInPaper>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.CommentInPapers
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<CommentInPaper>(this.recordCount).ToList<CommentInPaper>();
            }
            else
            {
                source = (from x in this.context.CommentInPapers
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<CommentInPaper>(this.recordCount).ToList<CommentInPaper>();
            }
            if ((source == null) || (source.Count<CommentInPaper>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<CommentInPaper>().ModifyDate + "***") + source.LastOrDefault<CommentInPaper>().ModifyDate + "***";
            foreach (CommentInPaper paper in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, paper.Id, "^^^", paper.Desk, "^^^", paper.PaperId, "^^^", paper.UserId, "^^^", paper.Date, "^^^", paper.CommentId, "^^^0***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllCommentInFroum(string fromDate, string endDate, int isRefresh)
        {
            List<CommentInFroum> source = null;
            string str = "";
            str = " CommentInFroum***Id^^^Desk^^^FroumId^^^UserId^^^Date^^^CommentId^^^NumofLike^^^NumofDisLike^^^Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.CommentInFroums
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<CommentInFroum>(this.recordCount).ToList<CommentInFroum>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.CommentInFroums
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<CommentInFroum>(this.recordCount).ToList<CommentInFroum>();
            }
            else
            {
                source = (from x in this.context.CommentInFroums
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<CommentInFroum>(this.recordCount).ToList<CommentInFroum>();
            }
            if ((source == null) || (source.Count<CommentInFroum>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<CommentInFroum>().ModifyDate + "***") + source.LastOrDefault<CommentInFroum>().ModifyDate + "***";
            foreach (CommentInFroum froum in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { 
                    obj2, froum.ID, "^^^", froum.Desk, "^^^", froum.FroumId, "^^^", froum.UserId, "^^^", froum.Date, "^^^", froum.CommentId, "^^^", froum.NumofLike, "^^^", froum.NumofDisLike, 
                    "^^^0***"
                 });
            }
            return str;
        }

        [WebMethod]
        public string getAllCommentInObject(string fromDate, string endDate, int isRefresh)
        {
            List<CommentInObject> source = null;
            string str = "";
            str = " CommentInObject***Id^^^Desk^^^ObjectId^^^UserId^^^Date^^^ CommentId ^^^ Seen ***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.CommentInObjects
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<CommentInObject>(this.recordCount).ToList<CommentInObject>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.CommentInObjects
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<CommentInObject>(this.recordCount).ToList<CommentInObject>();
            }
            else
            {
                source = (from x in this.context.CommentInObjects
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<CommentInObject>(this.recordCount).ToList<CommentInObject>();
            }
            if ((source == null) || (source.Count<CommentInObject>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<CommentInObject>().ModifyDate + "***") + source.LastOrDefault<CommentInObject>().ModifyDate + "***";
            foreach (CommentInObject obj2 in source)
            {
                object obj3 = str;
                str = string.Concat(new object[] { obj3, obj2.Id, "^^^", obj2.Desk, "^^^", obj2.ObjectId, "^^^", obj2.UserId, "^^^", obj2.Date, "^^^", obj2.CommentId, "^^^0***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllCommentInPost(string fromDate, string endDate, int isRefresh)
        {
            List<CommentInPost> source = null;
            string str = "";
            str = " CommentInPost***Id^^^Description^^^PostId^^^UserId^^^Date^^^ CommentId***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.CommentInPosts
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<CommentInPost>(this.recordCount).ToList<CommentInPost>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.CommentInPosts
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<CommentInPost>(this.recordCount).ToList<CommentInPost>();
            }
            else
            {
                source = (from x in this.context.CommentInPosts
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<CommentInPost>(this.recordCount).ToList<CommentInPost>();
            }
            if ((source == null) || (source.Count<CommentInPost>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<CommentInPost>().ModifyDate + "***") + source.LastOrDefault<CommentInPost>().ModifyDate + "***";
            foreach (CommentInPost obj2 in source)
            {
                
                object obj3 = str;
                str = string.Concat(new object[] { obj3, obj2.Id, "^^^", obj2.Description, "^^^", obj2.PostId, "^^^", obj2.UserId, "^^^", obj2.ModifyDate, "^^^", obj2.CommentId, "***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllLikeInPost(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInPost> source = null;
            string str = "";
            str = " LikeInPost***Id^^^UserId^^^PostId^^^Date^^^CommentId***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.LikeInPosts
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<LikeInPost>(this.recordCount).ToList<LikeInPost>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.LikeInPosts
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInPost>(this.recordCount).ToList<LikeInPost>();
            }
            else
            {
                source = (from x in this.context.LikeInPosts
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInPost>(this.recordCount).ToList<LikeInPost>();
            }
            if ((source == null) || (source.Count<LikeInPost>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<LikeInPost>().ModifyDate + "***") + source.LastOrDefault<LikeInPost>().ModifyDate + "***";
            foreach (LikeInPost obj2 in source)
            {

                object obj3 = str;
                str = string.Concat(new object[] { obj3, obj2.Id, "^^^", obj2.UserId, "^^^", obj2.PostId, "^^^", obj2.ModifyDate, "^^^", obj2.CommentId, "***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllLikeInCommentPost(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInCommentPost> source = null;
            string str = "";
            str = " LikeInCommentPost***Id^^^CommentId^^^UserId^^^IsLike^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.LikeInCommentPosts
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<LikeInCommentPost>(this.recordCount).ToList<LikeInCommentPost>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.LikeInCommentPosts
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInCommentPost>(this.recordCount).ToList<LikeInCommentPost>();
            }
            else
            {
                source = (from x in this.context.LikeInCommentPosts
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInCommentPost>(this.recordCount).ToList<LikeInCommentPost>();
            }
            if ((source == null) || (source.Count<LikeInCommentPost>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<LikeInCommentPost>().ModifyDate + "***") + source.LastOrDefault<LikeInCommentPost>().ModifyDate + "***";
            foreach (LikeInCommentPost obj2 in source)
            {

                object obj3 = str;
                str = string.Concat(new object[] { obj3, obj2.Id, "^^^", obj2.CommentId, "^^^", obj2.UserId, "^^^", obj2.IsLike, "^^^", obj2.ModifyDate, "***" });
            }
            return str;
        }


        [WebMethod]
        public string getAllFroum(string fromDate, string endDate, int isRefresh)
        {
            List<Froum> source = null;
            string str = "";
            str = " Froum***Id^^^Title^^^Description^^^UserId^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Froums
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<Froum>(this.recordCount).ToList<Froum>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Froums
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Froum>(this.recordCount).ToList<Froum>();
            }
            else
            {
                source = (from x in this.context.Froums
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Froum>(this.recordCount).ToList<Froum>();
            }
            if ((source == null) || (source.Count<Froum>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Froum>().ModifyDate + "***") + source.LastOrDefault<Froum>().ModifyDate + "***";
            foreach (Froum froum in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, froum.Id, "^^^", froum.Title, "^^^", froum.Description, "^^^", froum.UserId, "^^^", froum.Date, "***" });
            }
            return str;
        }

        [WebMethod]
        public List<byte[]> getAllImage(string tableName, int id, string fromDate1, string fromDate2, string fromDate3)
        {
            return new List<byte[]>(3) { this.getObject1Image(tableName, id, fromDate1), this.getObject2Image(tableName, id, fromDate2), this.getObject3Image(tableName, id, fromDate3) };
        }

        [WebMethod]
        public string getAllImageServerDate(string fromDate, string endDate, int isRefresh)
        {
            string str = "";
            if ((fromDate != null) && (fromDate != ""))
            {
                Service.Object obj2 = (from x in this.context.Objects
                                       where x.Id == Convert.ToInt32(fromDate)
                                       select x).FirstOrDefault<Service.Object>();
                str = obj2.Image1Date + "-" + obj2.Image2Date + "-" + obj2.Image3Date + "-";
            }
            return str;
        }

        [WebMethod]
        public string getAllLikeInComment(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInComment> source = null;
            string str = "";
            str = " LikeInComment***Id^^^CommentId^^^UserId^^^IsLike^^^ModifyDate^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.LikeInComments
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<LikeInComment>(this.recordCount).ToList<LikeInComment>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.LikeInComments
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInComment>(this.recordCount).ToList<LikeInComment>();
            }
            else
            {
                source = (from x in this.context.LikeInComments
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInComment>(this.recordCount).ToList<LikeInComment>();
            }
            if ((source == null) || (source.Count<LikeInComment>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<LikeInComment>().ModifyDate + "***") + source.LastOrDefault<LikeInComment>().ModifyDate + "***";
            foreach (LikeInComment comment in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, comment.ID, "^^^", comment.CommentId, "^^^", comment.UserId, "^^^", comment.IsLike, "^^^", comment.ModifyDate, "^^^", comment.Date, "***"});
            }
            return str;
        }

        [WebMethod]
        public string getAllLikeInCommentObject(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInCommentObject> source = null;
            string str = "";
            str = " LikeInCommentObject***Id^^^CommentId^^^UserId^^^IsLike^^^ModifyDate***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.LikeInCommentObjects
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<LikeInCommentObject>(this.recordCount).ToList<LikeInCommentObject>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.LikeInCommentObjects
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInCommentObject>(this.recordCount).ToList<LikeInCommentObject>();
            }
            else
            {
                source = (from x in this.context.LikeInCommentObjects
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInCommentObject>(this.recordCount).ToList<LikeInCommentObject>();
            }
            if ((source == null) || (source.Count<LikeInCommentObject>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<LikeInCommentObject>().ModifyDate + "***") + source.LastOrDefault<LikeInCommentObject>().ModifyDate + "***";
            foreach (LikeInCommentObject obj2 in source)
            {
                object obj3 = str;
                str = string.Concat(new object[] { obj3, obj2.Id, "^^^", obj2.CommentId, "^^^", obj2.UserId, "^^^", obj2.IsLike, "^^^", obj2.ModifyDate, "***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllLikeInFroum(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInFroum> source = null;
            string str = "";
            str = "LikeInFroum***Id^^^UserId^^^FroumId^^^Date^^^CommentId^^^Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.LikeInFroums
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<LikeInFroum>(this.recordCount).ToList<LikeInFroum>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.LikeInFroums
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInFroum>(this.recordCount).ToList<LikeInFroum>();
            }
            else
            {
                source = (from x in this.context.LikeInFroums
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInFroum>(this.recordCount).ToList<LikeInFroum>();
            }
            if ((source == null) || (source.Count<LikeInFroum>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<LikeInFroum>().ModifyDate + "***") + source.LastOrDefault<LikeInFroum>().ModifyDate + "***";
            foreach (LikeInFroum froum in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, froum.Id, "^^^", froum.UserId, "^^^", froum.FroumId, "^^^", froum.Date, "^^^", froum.CommentId, "^^^0***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllLikeInObject(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInObject> source = null;
            string str = "";
            str = " LikeInObject***Id^^^UserId^^^PaperId^^^Date^^^CommentId^^^Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.LikeInObjects
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<LikeInObject>(this.recordCount).ToList<LikeInObject>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.LikeInObjects
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInObject>(this.recordCount).ToList<LikeInObject>();
            }
            else
            {
                source = (from x in this.context.LikeInObjects
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInObject>(this.recordCount).ToList<LikeInObject>();
            }
            if ((source == null) || (source.Count<LikeInObject>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<LikeInObject>().ModifyDate + "***") + source.LastOrDefault<LikeInObject>().ModifyDate + "***";
            foreach (LikeInObject obj2 in source)
            {
                object obj3 = str;
                str = string.Concat(new object[] { obj3, obj2.Id, "^^^", obj2.UserId, "^^^", obj2.ObjectId, "^^^", obj2.Date, "^^^", obj2.CommentId, "^^^0***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllLikeInPaper(string fromDate, string endDate, int isRefresh)
        {
            List<LikeInPaper> source = null;
            string str = "";
            str = "LikeInPaper***Id^^^UserId^^^PaperId^^^Date^^^CommentId^^^Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.LikeInPapers
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<LikeInPaper>(this.recordCount).ToList<LikeInPaper>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.LikeInPapers
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInPaper>(this.recordCount).ToList<LikeInPaper>();
            }
            else
            {
                source = (from x in this.context.LikeInPapers
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<LikeInPaper>(this.recordCount).ToList<LikeInPaper>();
            }
            if ((source == null) || (source.Count<LikeInPaper>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<LikeInPaper>().ModifyDate + "***") + source.LastOrDefault<LikeInPaper>().ModifyDate + "***";
            foreach (LikeInPaper paper in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, paper.Id, "^^^", paper.UserId, "^^^", paper.PaperId, "^^^", paper.Date, "^^^", paper.CommentId, "^^^0***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllNews(string fromDate, string endDate, int isRefresh)
        {
            List<New> source = null;
            string str = "";
            str = " News***Id^^^Title^^^Description***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.News
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<New>(this.recordCount).ToList<New>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.News
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<New>(this.recordCount).ToList<New>();
            }
            else
            {
                source = (from x in this.context.News
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<New>(this.recordCount).ToList<New>();
            }
            if ((source == null) || (source.Count<New>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<New>().ModifyDate + "***") + source.LastOrDefault<New>().ModifyDate + "***";
            foreach (New new2 in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, new2.Id, "^^^", new2.Title, "^^^", new2.Description, "***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllObject(string fromDate, string endDate, int isRefresh)
        {
            List<Service.Object> source = null;
            string str = "";
            str = " Object***Id^^^Name^^^Phone^^^Email^^^Fax^^^Description^^^Cellphone^^^Address^^^Pdf1^^^Pdf2^^^Pdf3^^^Pdf4^^^ObjectTypeId^^^ObjectBrandTypeId^^^Facebook^^^Instagram^^^LinkedIn^^^Google^^^Site^^^Twitter^^^ParentId^^^rate^^^serverDate^^^MainObjectId^^^ObjectId^^^UserId^^^Date^^^IsActive^^^AgencyService***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Objects
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<Service.Object>(this.recordCount).ToList<Service.Object>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Objects
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Service.Object>(this.recordCount).ToList<Service.Object>();
            }
            else
            {
                source = (from x in this.context.Objects
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Service.Object>(this.recordCount).ToList<Service.Object>();
            }
            if ((source == null) || (source.Count<Service.Object>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Service.Object>().ModifyDate + "***") + source.LastOrDefault<Service.Object>().ModifyDate + "***";
            foreach (Service.Object obj2 in source)
            {
                object obj3 = str;
                str = string.Concat(new object[] { 
                    obj3, obj2.Id, "^^^", obj2.Name, "^^^", obj2.Phone, "^^^", obj2.Email, "^^^", obj2.Fax, "^^^", obj2.Description, "^^^", obj2.Cellphone, "^^^", obj2.Address, 
                    "^^^", obj2.Pdf1, "^^^", obj2.Pdf2, "^^^", obj2.Pdf3, "^^^", obj2.Pdf4, "^^^", obj2.ObjectTypeId, "^^^", obj2.ObjectBrandTypeId, "^^^", obj2.Facebook, "^^^", obj2.Instagram, 
                    "^^^", obj2.LinkedIn, "^^^", obj2.Google, "^^^", obj2.Site, "^^^", obj2.Twitter, "^^^", obj2.ParentId, "^^^", obj2.rate, "^^^", obj2.Date, "^^^", obj2.MainObjectId, 
                    "^^^", obj2.ObjectId, "^^^", obj2.UserId, "^^^", obj2.Date, "^^^", obj2.IsActive, "^^^", obj2.AgencyService, "***"
                 });
            }
            return str;
        }

        [WebMethod]
        public string getAllObjectInCity(string fromDate, string endDate, int isRefresh)
        {
            List<ObjectInCity> source = null;
            string str = "";
            str = " ObjectInCity***Id^^^ObjectId^^^CityId^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.ObjectInCities
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).ToList<ObjectInCity>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.ObjectInCities
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).ToList<ObjectInCity>();
            }
            else
            {
                source = (from x in this.context.ObjectInCities
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).ToList<ObjectInCity>();
            }
            if ((source == null) || (source.Count<ObjectInCity>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<ObjectInCity>().ModifyDate + "***") + source.LastOrDefault<ObjectInCity>().ModifyDate + "***";
            foreach (ObjectInCity city in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, city.Id, "^^^", city.ObjectId, "^^^", city.CityId, "^^^", city.ModifyDate, "***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllPaper(string fromDate, string endDate, int isRefresh)
        {
            List<Paper> source = null;
            string str = "";
            str = " Paper***Id^^^Title^^^Context^^^UserId^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Papers
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<Paper>(this.recordCount).ToList<Paper>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Papers
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Paper>(this.recordCount).ToList<Paper>();
            }
            else
            {
                source = (from x in this.context.Papers
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Paper>(this.recordCount).ToList<Paper>();
            }
            if ((source == null) || (source.Count<Paper>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Paper>().ModifyDate + "***") + source.LastOrDefault<Paper>().ModifyDate + "***";
            foreach (Paper paper in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, paper.Id, "^^^", paper.Title, "^^^", paper.Context, "^^^", paper.UserId, "^^^", paper.Date, "***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllTicket(string fromDate, string endDate, int isRefresh)
        {
            List<Ticket> source = null;
            string str = "";
            str = " Ticket***Id^^^Title^^^Desc^^^UserId^^^ Date^^^ TypeId^^^Name^^^Email^^^Mobile^^^Phone^^^Fax^^^ProvinceId^^^UName^^^UEmail^^^UPhonnumber^^^UFax^^^UAdress^^^UMobile^^^Seen^^^Day***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Tickets
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<Ticket>(this.recordCount).ToList<Ticket>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Tickets
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Ticket>(this.recordCount).ToList<Ticket>();
            }
            else
            {
                source = (from x in this.context.Tickets
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Ticket>(this.recordCount).ToList<Ticket>();
            }
            if ((source == null) || (source.Count<Ticket>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Ticket>().ModifyDate + "***") + source.LastOrDefault<Ticket>().ModifyDate + "***";
            foreach (Ticket ticket in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { 
                    obj2, ticket.Id, "^^^", ticket.Title, "^^^", ticket.Desc, "^^^", ticket.UserId, "^^^", ticket.Date, "^^^", ticket.TypeId, "^^^", ticket.Name, "^^^", ticket.Email, 
                    "^^^", ticket.Mobile, "^^^", ticket.Phone, "^^^", ticket.Fax, "^^^", ticket.ProvinceId, " ^^^", ticket.UName, "^^^", ticket.UEmail, "^^^", ticket.UPhonnumber, "^^^", ticket.UFax, 
                    "^^^", ticket.UAdress, "^^^", ticket.UMobile, "^^^0^^^"+ticket.Day+"***"
                 });
            }
            return str;
        }

        [WebMethod]
        public string getAllUpdateDetails(string fromDate, string endDate, int isRefresh)
        {
            string[] strArray = fromDate.Split(new char[] { '-' });
            string[] strArray2 = endDate.Split(new char[] { '-' });
            string str = "";
            string str2 = "";
            str2 = this.getAllCmtInPaper(strArray[0], strArray2[0], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllCommentInFroum(strArray[1], strArray2[1], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllCommentInObject(strArray[2], strArray2[2], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllLikeInFroum(strArray[3], strArray2[3], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllLikeInObject(strArray[4], strArray2[4], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllLikeInPaper(strArray[5], strArray2[5], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllLikeInComment(strArray[6], strArray2[6], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllLikeInCommentObject(strArray[7], strArray2[7], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllObjectInCity(strArray[8], strArray2[8], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }

            str2 = this.getAllCommentInPost(strArray[1], strArray2[1], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }

            str2 = this.getAllLikeInPost(strArray[1], strArray2[1], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllLikeInCommentPost(strArray[1], strArray2[1], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            return str;
        }

        [WebMethod]
        public string getAllUpdateMaster(string fromDate, string endDate, int isRefresh)
        {
            string[] strArray = fromDate.Split(new char[] { '-' });
            string[] strArray2 = endDate.Split(new char[] { '-' });
            string str = "";
            string str2 = "";
            str2 = this.getAllAnad(strArray[0], strArray2[0], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllFroum(strArray[1], strArray2[1], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllNews(strArray[2], strArray2[2], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllObject(strArray[3], strArray2[3], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllPaper(strArray[4], strArray2[4], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllTicket(strArray[5], strArray2[5], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllUser(strArray[6], strArray2[6], isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllObjectInCity("", "", isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }
            str2 = this.getAllPost("", "", isRefresh);
            if (str2 != "")
            {
                str = str + str2 + "&&&";
            }


            return str;
        }

        [WebMethod]
        public string getAllUser(string fromDate, string endDate, int isRefresh)
        {
            List<User> source = null;
            string str = "";
            str = "Users***Id^^^Name^^^Email^^^Password^^^Phonenumber^^^mobailenumber^^^Faxnumber^^^Address^^^Date^^^ShowInfoItem^^^BirthDay^^^CityId***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Users
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<User>(this.recordCount).ToList<User>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Users
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<User>(this.recordCount).ToList<User>();
            }
            else
            {
                source = (from x in this.context.Users
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<User>(this.recordCount).ToList<User>();
            }
            if ((source == null) || (source.Count<User>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<User>().ModifyDate + "***") + source.LastOrDefault<User>().ModifyDate + "***";
            foreach (User user in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { 
                    obj2, user.Id, "^^^", user.Name, "^^^", user.Email, "^^^", user.Password, "^^^", user.Phonenumber, "^^^", user.Mobailenumber, "^^^", user.Faxnumber, "^^^", user.Address, 
                    "^^^", user.Date,"^^^",user.ShowInfoItem,"^^^",user.BirthDay,"^^^",user.CityId ,"***"
                 });
            }
            return str;
        }

        [WebMethod]
        public string getAllPost(string fromDate, string endDate, int isRefresh)
        {
            List<Post> source = null;
            string str = "";
            str = "Post***Id^^^UserId^^^objectId^^^Description^^^Seen^^^Submit^^^Date^^^seenBefore***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Posts
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0)
                              orderby x.ModifyDate descending
                              select x).Take<Post>(this.recordCount).ToList<Post>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Posts
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Post>(this.recordCount).ToList<Post>();
            }
            else
            {
                source = (from x in this.context.Posts
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)
                          orderby x.ModifyDate descending
                          select x).Take<Post>(this.recordCount).ToList<Post>();
            }
            if ((source == null) || (source.Count<Post>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Post>().ModifyDate + "***") + source.LastOrDefault<Post>().ModifyDate + "***";
            foreach (Post post in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { 
                    obj2, post.Id, "^^^", post.UserId, "^^^",post.ObjectId ,"^^^", post.Description, "^^^", post.Seen, "^^^", post.Submit, "^^^", post.Date, "^^^", post.seenBefore,"***"
                 });
            }
            return str;
        }

        public string getAllPostByUserId(string fromDate, string endDate, int isRefresh,int userId)
        {
            List<Post> source = null;
            string str = "";
            str = "Post***Id^^^UserId^^^Description^^^Seen^^^Submit^^^Date^^^seenBefore***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Posts
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0) && x.UserId == userId
                              orderby x.ModifyDate descending
                              select x).Take<Post>(this.recordCount).ToList<Post>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Posts
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).Take<Post>(this.recordCount).ToList<Post>();
            }
            else
            {
                source = (from x in this.context.Posts
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).Take<Post>(this.recordCount).ToList<Post>();
            }
            if ((source == null) || (source.Count<Post>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Post>().ModifyDate + "***") + source.LastOrDefault<Post>().ModifyDate + "***";
            foreach (Post post in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { 
                    obj2, post.Id, "^^^", post.UserId, "^^^", post.Description, "^^^", post.Seen, "^^^", post.Submit, "^^^", post.Date, "^^^", post.seenBefore,"***"
                 });
            }
            return str;
        }

        [WebMethod]
        public string getAllPostByObjectId(string fromDate, string endDate, int isRefresh, int objectId)
        {
            List<Post> source = null;
            string str = "";
            str = "Post***Id^^^UserId^^^Description^^^Seen^^^Submit^^^Date^^^seenBefore***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Posts
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0) && x.ObjectId == objectId
                              orderby x.ModifyDate descending
                              select x).Take<Post>(this.recordCount).ToList<Post>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Posts
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0) && x.ObjectId == objectId
                          orderby x.ModifyDate descending
                          select x).Take<Post>(this.recordCount).ToList<Post>();
            }
            else
            {
                source = (from x in this.context.Posts
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0) && x.ObjectId == objectId
                          orderby x.ModifyDate descending
                          select x).Take<Post>(this.recordCount).ToList<Post>();
            }
            if ((source == null) || (source.Count<Post>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Post>().ModifyDate + "***") + source.LastOrDefault<Post>().ModifyDate + "***";
            foreach (Post post in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { 
                    obj2, post.Id, "^^^", post.UserId, "^^^", post.Description, "^^^", post.Seen, "^^^", post.Submit, "^^^", post.Date, "^^^", post.seenBefore,"***"
                 });
            }
            return str;
        }

        [WebMethod]
        public string getAllPostByUserIdAndObjectId(string fromDate, string endDate, int isRefresh,int userId, int objectId)
        {
            List<Post> source = null;
            string str = "";
            str = "Post***Id^^^UserId^^^Description^^^Seen^^^Submit^^^Date^^^seenBefore***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Posts
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0) && x.ObjectId == objectId && x.UserId == userId
                              orderby x.ModifyDate descending
                              select x).Take<Post>(this.recordCount).ToList<Post>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Posts
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0) && x.ObjectId == objectId && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).Take<Post>(this.recordCount).ToList<Post>();
            }
            else
            {
                source = (from x in this.context.Posts
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0) && x.ObjectId == objectId && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).Take<Post>(this.recordCount).ToList<Post>();
            }
            if ((source == null) || (source.Count<Post>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Post>().ModifyDate + "***") + source.LastOrDefault<Post>().ModifyDate + "***";
            foreach (Post post in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { 
                    obj2, post.Id, "^^^", post.UserId, "^^^", post.Description, "^^^", post.Seen, "^^^", post.Submit, "^^^", post.Date, "^^^", post.seenBefore,"***"
                 });
            }
            return str;
        }


        private DateTime getCuurentDate()
        {
            return DateTime.Now;
        }

        [WebMethod]
        public string getCuurentServerDate()
        {
            return (DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString());
        }

        [WebMethod]
        public byte[] getAnadImage(string tableName, int id, string fromDate)
        {
            Service.Anad anad;
            byte[] buffer = new byte[1000];
            if ((fromDate == null) || ("" == fromDate))
            {
                anad = (from x in this.context.Anads
                        where x.Id == id
                        select x).FirstOrDefault<Service.Anad>();
                if ((anad != null) && (anad.Image != null))
                {
                    buffer = anad.Image.ToArray();
                }
                return buffer;
            }
            anad = (from x in this.context.Anads
                    where (x.ImageServerDate != null) && (fromDate.CompareTo(x.ImageServerDate) < 0)
                    select x).FirstOrDefault<Service.Anad>();
            if ((anad != null) && (anad.Image != null))
            {
                buffer = anad.Image.ToArray();
            }
            return buffer;
        }

        [WebMethod]
        public byte[] getPostImage(string tableName, int id, string fromDate)
        {
            Service.Post post;
            byte[] buffer = new byte[1000];
            post = (from x in this.context.Posts
                    where x.Id == id
                    select x).FirstOrDefault<Service.Post>();
            if ((post != null) && (post.Photo != null))
            {
                buffer = post.Photo.ToArray();
            }
            return buffer;
        }

        [WebMethod]
        public byte[] getObject1Image(string tableName, int id, string fromDate)
        {
            Service.Object obj2;
            byte[] buffer = new byte[1000];
            if ((fromDate == null) || ("".Equals(fromDate)))
            {
                obj2 = (from x in this.context.Objects
                        where x.Id == id
                        select x).FirstOrDefault<Service.Object>();
                if ((obj2 != null) && (obj2.Image1 != null))
                {
                    buffer = obj2.Image1.ToArray();
                }
            }
            else
            {
                obj2 = (from x in this.context.Objects
                        where x.Id == id && (x.Image1Date != null) && (fromDate.CompareTo(x.Image1Date) > 0)
                        select x).FirstOrDefault<Service.Object>();
                if ((obj2 != null) && (obj2.Image1 != null))
                {
                    buffer = obj2.Image1.ToArray();
                }
            }
            return buffer;
        }

        [WebMethod]
        public byte[] getObject2Image(string tableName, int id, string fromDate)
        {
            Service.Object obj2;
            byte[] buffer = new byte[1000];
            if ((fromDate == null) || ("".Equals(fromDate)))
            {
                obj2 = (from x in this.context.Objects
                        where x.Id == id
                        select x).FirstOrDefault<Service.Object>();
                if ((obj2 != null) && (obj2.Image2 != null))
                {
                    buffer = obj2.Image2.ToArray();
                }
            }
            else
            {
                obj2 = (from x in this.context.Objects
                        where x.Id == id && (x.Image2Date != null) && (fromDate.CompareTo(x.Image2Date) > 0)
                        select x).FirstOrDefault<Service.Object>();
                if ((obj2 != null) && (obj2.Image2 != null))
                {
                    buffer = obj2.Image2.ToArray();
                }
            }
            
            return buffer;
        }

        [WebMethod]
        public byte[] getObject3Image(string tableName, int id, string fromDate)
        {
            Service.Object obj2;
            byte[] buffer = new byte[1000];
            if ((fromDate == null) || ("".Equals(fromDate)))
            {
                obj2 = (from x in this.context.Objects
                        where x.Id == id
                        select x).FirstOrDefault<Service.Object>();
                if ((obj2 != null) && (obj2.Image3 != null))
                {
                    buffer = obj2.Image3.ToArray();
                }
            }
            else
            {
                obj2 = (from x in this.context.Objects
                        where x.Id == id && (x.Image3Date != null) && (fromDate.CompareTo(x.Image3Date) > 0)
                        select x).FirstOrDefault<Service.Object>();
                if ((obj2 != null) && (obj2.Image3 != null))
                {
                    buffer = obj2.Image3.ToArray(); 
                }
            }
            return buffer;
        }

        [WebMethod]
        public string getServerDateMilis()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        [WebMethod]
        public string getUserById(string Id)
        {
            List<User> source = null;
            string str = "";
            str = "Users***Id^^^Name^^^Email^^^Password^^^Phonenumber^^^mobailenumber^^^Faxnumber^^^Address^^^Date^^^ShowInfoItem^^^BirthDay^^^CityId***";
            string[] tempParams = Id.Split(new char[] { '-' });
            source = (from x in this.context.Users
                      where tempParams.Contains<string>(x.Id.ToString())
                      select x).ToList<User>();
            if ((source == null) || (source.Count<User>() <= 0))
            {
                str = "";
            }
            else
            {
                str = str + "***" + "***";
                foreach (User user in source)
                {
                    object obj2 = str;
                    str = string.Concat(new object[] { 
                        obj2, user.Id, "^^^", user.Name, "^^^", user.Email, "^^^", user.Password, "^^^", user.Phonenumber, "^^^", user.Mobailenumber, "^^^", user.Faxnumber, "^^^", user.Address, 
                        "^^^", user.Date,"^^^",user.ShowInfoItem,"^^^",user.BirthDay,"^^^",user.CityId, "***"
                     });
                }
            }
            return (str + "&&&");
        }

        [WebMethod]
        public byte[] getUsersImage(int id, string fromDate)
        {
            User user;
            byte[] image = null;
            if ((fromDate == null) || ("" == fromDate))
            {
                user = (from x in this.context.Users
                        where x.Id == id
                        select x).FirstOrDefault<User>();
                if (user != null && user.Image != null)
                {
                    image = user.Image.ToArray();
                }
                return image;

            }
            user = (from x in this.context.Users
                    where (x.ImageServerDate != null) && (fromDate.CompareTo(x.ImageServerDate) < 0)
                    select x).FirstOrDefault<User>();
            if (user != null && user.Image != null)
            {
                image = user.Image.ToArray();
            }
            return image;
        }
        [WebMethod]
        public byte[] getTicketImage(int id, string fromDate)
        {
            Ticket ticket;
            byte[] image = null;
            if ((fromDate == null) || ("" == fromDate))
            {
                ticket = (from x in this.context.Tickets
                        where x.Id == id
                          select x).FirstOrDefault<Ticket>();
                if (ticket != null)
                {
                    image = ticket.Image;
                }
                return image;
            }
            ticket = (from x in this.context.Tickets
                    where (x.ImageServerDate != null) && (fromDate.CompareTo(x.ImageServerDate) < 0)
                      select x).FirstOrDefault<Ticket>();
            if (ticket != null)
            {
                image = ticket.Image;
            }
            return image;
        }

        [WebMethod]
        public string login(string phone, string password)
        {
            string str = "";
            IQueryable<User> source = from x in this.context.Users
                                      where phone.Equals(x.Mobailenumber) && password.Equals(x.Password)
                                      select x;
            if (source != null)
            {
                User user = source.FirstOrDefault<User>();
                if (user != null)
                {
                    str = "Users***Id^^^Name^^^Email^^^Password^^^Phonenumber^^^mobailenumber^^^Faxnumber^^^Address^^^Date^^^ShowInfoItem^^^BirthDay^^^CityId***";
                    object obj2 = str + "***" + "***";
                    str = string.Concat(new object[] { 
                        obj2, user.Id, "^^^", user.Name, "^^^", user.Email, "^^^", user.Password, "^^^", user.Phonenumber, "^^^", user.Mobailenumber, "^^^", user.Faxnumber, "^^^", user.Address, 
                        "^^^", user.Date,"^^^",user.ShowInfoItem,"^^^",user.BirthDay,"^^^",user.CityId, "***&&&"
                     });
                }
            }
            return str;
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
            Object o = (from c in context.Objects where c.Id == Convert.ToInt32(Id) select c).FirstOrDefault<Object>();
            string date = "";
            if (o != null && o.Image1Date!= null)
            {

                date = o.Image1Date;
            }
            return date;
        }
        [WebMethod]
        public string getObject2ImageDate(string Id)
        {
            Object o = (from c in context.Objects where c.Id == Convert.ToInt32(Id) select c).FirstOrDefault<Object>();
            string date = "";
            if (o != null && o.Image2Date != null)
            {

                date = o.Image2Date;
            }
            return date;
        }
        [WebMethod]
        public string getObject3ImageDate(string Id)
        {
            Object o = (from c in context.Objects where c.Id == Convert.ToInt32(Id) select c).FirstOrDefault<Object>();
            string date = "";
            if (o != null && o.Image3Date != null)
            {

                date = o.Image3Date;
            }
            return date;
        }

        [WebMethod]
        public string getUserImageDate(string Id)
        {
            User o = (from c in context.Users where c.Id == Convert.ToInt32(Id) select c).FirstOrDefault<User>();
            string date = "";
            if (o != null && o.ImageServerDate != null)
            {

                date = o.ImageServerDate;
            }
            return date;
        }

        [WebMethod]
        public string getPostImageDate(string Id)
        {
            Post o = (from c in context.Posts where c.Id == Convert.ToInt32(Id) select c).FirstOrDefault<Post>();
            string date = "";
            if (o != null && o.ImageServerDate != null)
            {

                date = o.ImageServerDate;
            }
            return date;
        }

        [WebMethod]
        public string getAnadImageDate(string Id)
        {
            Anad o = (from c in context.Anads where c.Id == Convert.ToInt32(Id) select c).FirstOrDefault<Anad>();
            return "";
        }

        [WebMethod]
        public string getTicketImageDate(string Id)
        {
            Ticket o = (from c in context.Tickets where c.Id == Convert.ToInt32(Id) select c).FirstOrDefault<Ticket>();
            string date = "";
            if (o != null && o.ImageServerDate != null)
            {

                date = o.ImageServerDate;
            }
            return date;
        }


        [WebMethod]
        public bool recoverPassword(string email)
        {
            try
            {
                string password = (from x in this.context.Users
                                   where x.Email == email
                                   select x).First<User>().Password;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public string register(int clientId, string username, string email, string password, string phone, string mobile, int fax, string address, string date)
        {
            try
            {
                if (this.context.Users.Any<User>(x => x.Mobailenumber == mobile))
                {
                    return "-10";
                }
                User entity = new User
                {
                    Name = username,
                    Email = email,
                    Password = password,
                    Phonenumber = phone,
                    Mobailenumber = mobile,
                    Faxnumber = new int?(fax),
                    Address = address,
                    Date = date
                };
                this.context.Users.InsertOnSubmit(entity);
                this.context.SubmitChanges();
                return entity.Id.ToString();
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        [WebMethod]
        public string regPhone(string phone)
        {
            string str = "";
            IQueryable<User> source = from x in this.context.Users
                                      where phone.Equals(x.Mobailenumber)
                                      select x;
            if (source != null)
            {
                User user = source.FirstOrDefault<User>();
                if (user != null)
                {
                    str = "Users***Id^^^Name^^^Email^^^Password^^^Phonenumber^^^mobailenumber^^^Faxnumber^^^Address^^^Date^^^ShowInfoItem^^^BirthDay^^^CityId***";
                    object obj2 = str;
                    str = string.Concat(new object[] { 
                        obj2, user.Id, "^^^", user.Name, "^^^", user.Email, "^^^", user.Password, "^^^", user.Phonenumber, "^^^", user.Mobailenumber, "^^^", user.Faxnumber, "^^^", user.Address, 
                        "^^^", user.Date,"^^^",user.ShowInfoItem,"^^^",user.BirthDay,"^^^",user.CityId, "***&&&"
                     });
                }
            }
            return str;
        }

        [WebMethod]
        public string Saving(string tableName, string param, string IsUpdate, string Id)
        {
            Exception exception;
            bool flag = Convert.ToInt32(IsUpdate) > 0;
            string str = "";
            int num = 0;
            string str2 = "";
            string str3 = "";
            string[] strArray = param.Split(new char[] { '-' });
            string str4 = "";
            string str5 = "";
            string str6 = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                if ((strArray[i] != null) && strArray[i].Contains(":"))
                {
                    str5 = strArray[i].Substring(0, strArray[i].IndexOf(":")) + ",";
                    str6 = strArray[i].Substring(0, strArray[i].IndexOf(":"));
                    str4 = strArray[i].Substring(strArray[i].IndexOf(":") + 1, (strArray[i].Length - strArray[i].IndexOf(":")) - 1);
                    if (flag)
                    {
                        if ((str4 != null) && (str4 != ""))
                        {
                            string str9 = str;
                            str = str9 + str6 + "='" + str4 + "',";
                        }
                    }
                    else
                    {
                        str2 = str2 + str5;
                        try
                        {
                            int num3 = Convert.ToInt32(str4);
                            str3 = str3 + str4 + ",";
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            str3 = str3 + "'" + str4 + "',";
                        }
                    }
                }
            }
            if ((str != null) && str.Contains(","))
            {
                str = str.Remove(str.LastIndexOf(","), 1);
            }
            if ((str2 != null) && str2.Contains(","))
            {
                str2 = str2.Remove(str2.LastIndexOf(","), 1);
            }
            if ((str3 != null) && str3.Contains(","))
            {
                str3 = str3.Remove(str3.LastIndexOf(","), 1);
            }
            if (flag)
            {
                if (str == "")
                {
                    num = -1;
                }
                else
                {
                    int num4 = this.context.ExecuteQuery<int>("UPDATE " + tableName + " SET " + str + " WHERE Id=" + Id, new object[0]).FirstOrDefault<int>();
                    try
                    {
                        num = Convert.ToInt32(num4);
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        num = -1;
                    }
                }
            }
            else if (str3 == "")
            {
                num = -1;
            }
            else
            {
                string query = "INSERT INTO " + tableName + " (" + str2 + ") VALUES (" + str3 + ") ; select scope_identity()";
                decimal num5 = this.context.ExecuteQuery<decimal>(query, new object[0]).First<decimal>();
                try
                {
                    num = Convert.ToInt32(num5);
                }
                catch (Exception exception3)
                {
                    exception = exception3;
                    num = -1;
                }
            }
            return num.ToString();
        }

        [WebMethod]
        public int savingImage(string tableName, string fieldName, string id, byte[] image)
        {
            return this.context.ExecuteCommand("Update [" + tableName + "] SET " + fieldName + "={0} , ImageServerDate = '"+getServerDateMilis()+"'  WHERE Id=" + id, new object[] { image });
        }

        [WebMethod]
        public string savingImage3(string tableName, string fieldName1, string fieldName2, string fieldName3, string id, byte[] image1, byte[] image2, byte[] image3)
        {
            string str = "Update [" + tableName + "] SET ";
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            object[] parameters = new object[3];
            string str2 = this.getServerDateMilis();
            if (image1 != null)
            {
                flag = true;
                str = (str + fieldName1 + "={0}, ") + " Image1Date=" + str2;
            }
            if (image2 != null)
            {
                flag2 = true;
                if (flag)
                {
                    str = str + " , ";
                }
                str = (str + fieldName2 + "={1}, ") + " Image2Date=" + str2;
            }
            if (image3 != null)
            {
                if (flag2 || flag)
                {
                    str = str + " , ";
                }
                flag3 = true;
                str = (str + fieldName3 + "={2}, ") + " Image3Date=" + str2;
            }
            if (flag)
            {
                parameters[0] = image1;
            }
            if (flag2)
            {
                parameters[1] = image2;
            }
            if (flag3)
            {
                parameters[2] = image3;
            }
            int num = this.context.ExecuteCommand(str + " WHERE Id=" + id, parameters);
            return str2;
        }

        [WebMethod]
        public string getAllTicketByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Ticket> source = null;
            string str = "";
            str = " Ticket***Id^^^Title^^^Desc^^^UserId^^^ Date^^^ TypeId^^^Name^^^Email^^^Mobile^^^Phone^^^Fax^^^ProvinceId^^^UName^^^UEmail^^^UPhonnumber^^^UFax^^^UAdress^^^UMobile^^^Seen^^^Day***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Tickets
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0) && x.UserId == userId
                              orderby x.ModifyDate descending
                              select x).ToList<Ticket>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Tickets
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).ToList<Ticket>();
            }
            else
            {
                source = (from x in this.context.Tickets
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).ToList<Ticket>();
            }
            if ((source == null) || (source.Count<Ticket>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Ticket>().ModifyDate + "***") + source.LastOrDefault<Ticket>().ModifyDate + "***";
            foreach (Ticket ticket in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { 
                    obj2, ticket.Id, "^^^", ticket.Title, "^^^", ticket.Desc, "^^^", ticket.UserId, "^^^", ticket.Date, "^^^", ticket.TypeId, "^^^", ticket.Name, "^^^", ticket.Email, 
                    "^^^", ticket.Mobile, "^^^", ticket.Phone, "^^^", ticket.Fax, "^^^", ticket.ProvinceId, " ^^^", ticket.UName, "^^^", ticket.UEmail, "^^^", ticket.UPhonnumber, "^^^", ticket.UFax, 
                    "^^^", ticket.UAdress, "^^^", ticket.UMobile, "^^^0^^^"+ticket.Day+"***"
                 });
            }
            return str;
        }
         
        [WebMethod]
        public string getAllObjectByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Service.Object> source = null;
            string str = "";
            str = " Object***Id^^^Name^^^Phone^^^Email^^^Fax^^^Description^^^Cellphone^^^Address^^^Pdf1^^^Pdf2^^^Pdf3^^^Pdf4^^^ObjectTypeId^^^ObjectBrandTypeId^^^Facebook^^^Instagram^^^LinkedIn^^^Google^^^Site^^^Twitter^^^ParentId^^^rate^^^serverDate^^^MainObjectId^^^ObjectId^^^UserId^^^Date^^^IsActive^^^AgencyService***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Objects
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0) && x.UserId == userId
                              orderby x.ModifyDate descending
                              select x).ToList<Service.Object>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Objects
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).ToList<Service.Object>();
            }
            else
            {
                source = (from x in this.context.Objects
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).ToList<Service.Object>();
            }
            if ((source == null) || (source.Count<Service.Object>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Service.Object>().ModifyDate + "***") + source.LastOrDefault<Service.Object>().ModifyDate + "***";
            foreach (Service.Object obj2 in source)
            {
                object obj3 = str;
                str = string.Concat(new object[] { 
                    obj3, obj2.Id, "^^^", obj2.Name, "^^^", obj2.Phone, "^^^", obj2.Email, "^^^", obj2.Fax, "^^^", obj2.Description, "^^^", obj2.Cellphone, "^^^", obj2.Address, 
                    "^^^", obj2.Pdf1, "^^^", obj2.Pdf2, "^^^", obj2.Pdf3, "^^^", obj2.Pdf4, "^^^", obj2.ObjectTypeId, "^^^", obj2.ObjectBrandTypeId, "^^^", obj2.Facebook, "^^^", obj2.Instagram, 
                    "^^^", obj2.LinkedIn, "^^^", obj2.Google, "^^^", obj2.Site, "^^^", obj2.Twitter, "^^^", obj2.ParentId, "^^^", obj2.rate, "^^^", obj2.Date, "^^^", obj2.MainObjectId, 
                    "^^^", obj2.ObjectId, "^^^", obj2.UserId, "^^^", obj2.Date, "^^^", obj2.IsActive, "^^^", obj2.AgencyService, "***"
                 });
            }
            return str;
        }

        [WebMethod]
        public string getAllFroumByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Froum> source = null;
            string str = "";
            str = " Froum***Id^^^Title^^^Description^^^UserId^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Froums
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0) && x.UserId == userId
                              orderby x.ModifyDate descending
                              select x).ToList<Froum>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Froums
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).ToList<Froum>();
            }
            else
            {
                source = (from x in this.context.Froums
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).ToList<Froum>();
            }
            if ((source == null) || (source.Count<Froum>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Froum>().ModifyDate + "***") + source.LastOrDefault<Froum>().ModifyDate + "***";
            foreach (Froum froum in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, froum.Id, "^^^", froum.Title, "^^^", froum.Description, "^^^", froum.UserId, "^^^", froum.Date, "***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllPaperByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Paper> source = null;
            string str = "";
            str = " Paper***Id^^^Title^^^Context^^^UserId^^^Date***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Papers
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0) && x.UserId == userId
                              orderby x.ModifyDate descending
                              select x).ToList<Paper>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Papers
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).ToList<Paper>();
            }
            else
            {
                source = (from x in this.context.Papers
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).ToList<Paper>();
            }
            if ((source == null) || (source.Count<Paper>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Paper>().ModifyDate + "***") + source.LastOrDefault<Paper>().ModifyDate + "***";
            foreach (Paper paper in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, paper.Id, "^^^", paper.Title, "^^^", paper.Context, "^^^", paper.UserId, "^^^", paper.Date, "***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllLikeInObjectByUserId(string fromDate, string endDate, int isRefresh,int userId)
        {
            List<LikeInObject> source = null;
            string str = "";
            str = " LikeInObject***Id^^^UserId^^^PaperId^^^Date^^^CommentId^^^Seen***";
            string currentDate = this.getServerDateMilis();
            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.LikeInObjects
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0) && x.UserId == userId
                              orderby x.ModifyDate descending
                              select x).ToList<LikeInObject>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.LikeInObjects
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).ToList<LikeInObject>();
            }
            else
            {
                source = (from x in this.context.LikeInObjects
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0)  && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).ToList<LikeInObject>();
            }
            if ((source == null) || (source.Count<LikeInObject>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<LikeInObject>().ModifyDate + "***") + source.LastOrDefault<LikeInObject>().ModifyDate + "***";
            foreach (LikeInObject obj2 in source)
            {
                object obj3 = str;
                str = string.Concat(new object[] { obj3, obj2.Id, "^^^", obj2.UserId, "^^^", obj2.ObjectId, "^^^", obj2.Date, "^^^", obj2.CommentId, "^^^0***" });
            }
            return str;
        }

        [WebMethod]
        public string getAllAnadByUserId(string fromDate, string endDate, int isRefresh, int userId)
        {
            List<Anad> source = null;
            string str = "";
            str = " Anad***Id^^^ObjectId^^^Date^^^ TypeId^^^ ProvinceId^^^ Seen^^^UserId***";
            string currentDate = this.getServerDateMilis();

            if (isRefresh > 0)
            {
                if ((fromDate != null) && (fromDate != ""))
                {
                    source = (from x in this.context.Anads
                              where (x.ModifyDate != null) && (fromDate.CompareTo(x.ModifyDate) < 0) && x.UserId == userId
                              orderby x.ModifyDate descending
                              select x).Take<Anad>(this.recordCount).ToList<Anad>();
                }
            }
            else if ((endDate != null) && (endDate != ""))
            {
                source = (from x in this.context.Anads
                          where (x.ModifyDate != null) && (endDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).Take<Anad>(this.recordCount).ToList<Anad>();
            }
            else
            {
                source = (from x in this.context.Anads
                          where (x.ModifyDate != null) && (currentDate.CompareTo(x.ModifyDate) >= 0) && x.UserId == userId
                          orderby x.ModifyDate descending
                          select x).Take<Anad>(this.recordCount).ToList<Anad>();
            }
            if ((source == null) || (source.Count<Anad>() <= 0))
            {
                return "";
            }
            str = (str + source.FirstOrDefault<Anad>().ModifyDate + "***") + source.LastOrDefault<Anad>().ModifyDate + "***";
            foreach (Anad anad in source)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, anad.Id, "^^^", anad.ObjectId, "^^^", anad.Date, "^^^", anad.TypeId, "^^^", anad.ProvinceId, "^^^0^^^",anad.UserId ,"***" });
            }
            return str;
        }

        [WebMethod]
        public long getAllVisit(int objectId, int typeId)
        {
            long count = 0;
            count = (from x in this.context.Visits
                      where x.ObjectId == objectId && x.TypeId == typeId
                      select x.UserId).LongCount();
            return count;
        }

        [WebMethod]
        public string getObjectByObjectId(int objectId, string fromDate, string toDate, int provinceId = 0, int cityId = 0, int agencyService = 0)
        {
            String str = " Object***Id^^^Name^^^Phone^^^Email^^^Fax^^^Description^^^Cellphone^^^Address^^^Pdf1^^^Pdf2^^^Pdf3^^^Pdf4^^^ObjectTypeId^^^ObjectBrandTypeId^^^Facebook^^^Instagram^^^LinkedIn^^^Google^^^Site^^^Twitter^^^ParentId^^^rate^^^serverDate^^^MainObjectId^^^ObjectId^^^UserId^^^Date^^^IsActive^^^AgencyService***";
            List<Object> source = null;
            source = (from obj in this.context.Objects 
                      join oic in this.context.ObjectInCities on obj.Id equals oic.ObjectId
                      where (obj.ModifyDate != null) && (fromDate.CompareTo(obj.ModifyDate) < 0)
                      && (toDate.CompareTo(obj.ModifyDate) >= 0)
                      && oic.CityId == cityId && obj.AgencyService == agencyService
                      orderby obj.ModifyDate descending
                      select obj).ToList<Object>();
         
            str = (str + source.FirstOrDefault<Service.Object>().ModifyDate + "***") + source.LastOrDefault<Service.Object>().ModifyDate + "***";
            foreach (Service.Object obj2 in source)
            {
                object obj3 = str;
                str = string.Concat(new object[] { 
                    obj3, obj2.Id, "^^^", obj2.Name, "^^^", obj2.Phone, "^^^", obj2.Email, "^^^", obj2.Fax, "^^^", obj2.Description, "^^^", obj2.Cellphone, "^^^", obj2.Address, 
                    "^^^", obj2.Pdf1, "^^^", obj2.Pdf2, "^^^", obj2.Pdf3, "^^^", obj2.Pdf4, "^^^", obj2.ObjectTypeId, "^^^", obj2.ObjectBrandTypeId, "^^^", obj2.Facebook, "^^^", obj2.Instagram, 
                    "^^^", obj2.LinkedIn, "^^^", obj2.Google, "^^^", obj2.Site, "^^^", obj2.Twitter, "^^^", obj2.ParentId, "^^^", obj2.rate, "^^^", obj2.Date, "^^^", obj2.MainObjectId, 
                    "^^^", obj2.ObjectId, "^^^", obj2.UserId, "^^^", obj2.Date, "^^^", obj2.IsActive, "^^^", obj2.AgencyService, "***"
                 });
            }
            return str;
        }
                    
    }
}
