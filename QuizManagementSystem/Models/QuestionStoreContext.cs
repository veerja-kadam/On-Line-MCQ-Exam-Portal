
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace QuizMIS.Models {

    public class QuestionStoreContext {
    
        public string ConnectionString {get;set;}

        public QuestionStoreContext(){  
            this.ConnectionString=null;  
        }   

        public QuestionStoreContext(string connectionString) {
            this.ConnectionString=connectionString;
        }

        private MySqlConnection GetConnection() {
            return new MySqlConnection(ConnectionString);
        }

        public List<Question> GetQuestions(int QuizId) {
            MySqlConnection Conn = GetConnection();
            Conn.Open();

            List<Question> q=new List<Question>();

            string query="select * from question inner join quiz on quiz.QuizId=question.QuestionId";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            var reader=cmd.ExecuteReader();
            while(reader.Read()) {
                q.Add(new Question(){
                    QuestionId=(int)reader["QuestionId"],
                    Title=(string)reader["Title"],
                    Option1=(string)reader["Option1"],
                    Option2=(string)reader["Option2"],
                    Option4=(string)reader["Option4"],
                    CorrectAnswer=(string)reader["CorrectAnswer"]
                });
            }

            return q;
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

        

        public void UpdateQuestion(int QuestionId,string Title,string Option1,string Option2,string Option3,string Option4,string CorrectAnswer) {
             MySqlConnection Conn = GetConnection();
            Conn.Open();

            string query="update question set Title=@v1 AND Option1=@v2 AND Option2=@v3 AND Option3=@v4 AND Option4=@v5 AND CorrectAnswer=@v6 where QuestionId=@v7";
            MySqlCommand cmd=new MySqlCommand(query,Conn);

            cmd.Parameters.AddWithValue("@v1",Title);
            cmd.Parameters.AddWithValue("@v2",Option1);
            cmd.Parameters.AddWithValue("@v3",Option2);
            cmd.Parameters.AddWithValue("@v4",Option3);
            cmd.Parameters.AddWithValue("@v5",Option4);
            cmd.Parameters.AddWithValue("@v6",CorrectAnswer);
            cmd.Parameters.AddWithValue("@v7",QuestionId);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}