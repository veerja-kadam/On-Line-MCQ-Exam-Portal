
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace QuizMIS.Models {

    public class QuizStoreContext {
    
        public string ConnectionString {get;set;}

        public QuizStoreContext() {  
            this.ConnectionString=null;  
        }   

        public QuizStoreContext(string connectionString) {
            this.ConnectionString=connectionString;
        }

        private MySqlConnection GetConnection() {
            return new MySqlConnection(ConnectionString);
        }

        public List<Question> getQuestions(int QuizId) {
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            string query="select * from question where QuizId=@v1";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",QuizId);
            cmd.Prepare();

            var reader=cmd.ExecuteReader();
            List<Question>q=new List<Question>();

            while(reader.Read()) {
                q.Add(new Question(){
                    QuestionId=(int)reader["QuestionId"],
                    Title=(string)reader["Title"],
                    Option1=(string)reader["Option1"],
                    Option2=(string)reader["Option2"],
                    Option3=(string)reader["Option3"],
                    Option4=(string)reader["Option4"],
                    CorrectAnswer=(string)reader["CorrectAnswer"],
                    Points=(int)reader["Points"]
                });
            }

            return q;
        }

        public void GetQuizTimings(int QuizId,ref string StartTime,ref string EndTime, ref string QuizTitle) {
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            string query="select QuizTitle,StartTime,EndTime from quiz where QuizId=@v1";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",QuizId);
            cmd.Prepare();

            var reader=cmd.ExecuteReader();
            while(reader.Read()) {
                if(reader["StartTime"]!=DBNull.Value)
                    StartTime=(string)reader["StartTime"];
                if(reader["EndTime"]!=DBNull.Value)
                    EndTime=(string)reader["EndTime"];
                if(reader["QuizTitle"]!=DBNull.Value)
                    QuizTitle=(string)reader["QuizTitle"];
            }
        }
        public void UpdateTiming(int QuizId,string StartTime,string EndTime, string QuizTitle) {
          
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            string query="update quiz set StartTime=@v1 , EndTime=@v2 , QuizTitle=@v4 where QuizId=@v3";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",StartTime);
            cmd.Parameters.AddWithValue("@v2",EndTime);
            cmd.Parameters.AddWithValue("@v3",QuizId);
            cmd.Parameters.AddWithValue("@v4",QuizTitle);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        public void AddQuestion(int QuizId,string Title,string Option1,string Option2,string Option3,string Option4,string CorrectAnswer,int Points) {
            MySqlConnection Conn=GetConnection();
            Conn.Open();

            string query="insert into question (Title,Option1,Option2,Option3,Option4,CorrectAnswer,Points,QuizId) values(@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8)";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",Title);
            cmd.Parameters.AddWithValue("@v2",Option1);
            cmd.Parameters.AddWithValue("@v3",Option2);
            cmd.Parameters.AddWithValue("@v4",Option3);
            cmd.Parameters.AddWithValue("@v5",Option4);
            cmd.Parameters.AddWithValue("@v6",CorrectAnswer);
            cmd.Parameters.AddWithValue("@v7",Points);
            cmd.Parameters.AddWithValue("@v8",QuizId);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void DeleteQuestion(int QuestionId) {
            MySqlConnection Conn = GetConnection();
            Conn.Open();

            string query="delete from question where QuestionId=@v1";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",QuestionId);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public Question GetQuestionDetails(int QuestionId) {
            MySqlConnection Conn = GetConnection();
            Conn.Open();

            string query="select * from question where QuestionId=@v1";
            MySqlCommand cmd=new MySqlCommand(query,Conn);
            cmd.Parameters.AddWithValue("@v1",QuestionId);

            cmd.Prepare();
            var reader=cmd.ExecuteReader();

            Question q=new Question();
            while(reader.Read()) {
                q.QuestionId=(int)reader["QuestionId"];
                q.Title=(string)reader["Title"];
                q.Option1=(string)reader["Option1"];
                q.Option2=(string)reader["Option2"];
                q.Option3=(string)reader["Option3"];
                q.Option4=(string)reader["Option4"];
                q.CorrectAnswer=(string)reader["CorrectAnswer"];
                q.Points=(int)reader["Points"];
            }
            return q;
        }

         public void UpdateQuestion(int QuestionId,string Title,string Option1,string Option2,string Option3,string Option4,string CorrectAnswer,int Points) {
             Console.WriteLine(QuestionId);
             Console.WriteLine(Title);
             MySqlConnection Conn = GetConnection();
            Conn.Open();

            string query="update question set Title=@v1 ,  Option1=@v2 ,  Option2=@v3 ,  Option3=@v4 ,  Option4=@v5 ,  CorrectAnswer=@v6 ,  Points=@v8 where QuestionId=@v7";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",Title);
            cmd.Parameters.AddWithValue("@v2",Option1);
            cmd.Parameters.AddWithValue("@v3",Option2);
            cmd.Parameters.AddWithValue("@v4",Option3);
            cmd.Parameters.AddWithValue("@v5",Option4);
            cmd.Parameters.AddWithValue("@v6",CorrectAnswer);
            cmd.Parameters.AddWithValue("@v7",QuestionId);
            cmd.Parameters.AddWithValue("@v8",Points);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}