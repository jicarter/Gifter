using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Gifter.Models;
using Gifter.Utils;

namespace Gifter.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT Id, [Name], Email, ImageUrl, DateCreated
                            FROM UserProfile
                        ORDER BY DateCreated";

                    var reader = cmd.ExecuteReader();

                    var userprofiles = new List<UserProfile>();
                    while (reader.Read())
                    {
                        userprofiles.Add(new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),

                        });
                    }

                    reader.Close();

                    return userprofiles;
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                           SELECT Id, [Name], Email, ImageUrl, DateCreated
                            FROM UserProfile
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    UserProfile userprofile = null;
                    if (reader.Read())
                    {
                        userprofile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),

                        };
                    }

                    reader.Close();

                    return userprofile;
                }
            }
        }

        public void Add(UserProfile userprofile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile ([Name], Email, ImageUrl, DateCreated)
                        OUTPUT INSERTED.ID
                        VALUES (@Name, @Email, @ImageUrl, @DateCreated)";

                    DbUtils.AddParameter(cmd, "@Name", userprofile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userprofile.Email);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userprofile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@DateCreated", userprofile.DateCreated);


                    userprofile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile userprofile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                           SET Name = @Name,
                               Email = @Email,
                               ImageUrl = @ImageUrl,
                               DateCreated = @DateCreated,
                             
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", userprofile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userprofile.Email);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userprofile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userprofile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@DateCreated", userprofile.DateCreated);
                    DbUtils.AddParameter(cmd, "@Id", userprofile.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UserProfile GetUserProfileByIdWithPosts(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT up.Name, up.Bio, up.Email, up.DateCreated AS UserProfileDateCreated,
                       up.ImageUrl AS UserProfileImageUrl,
                        p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated,
                       p.ImageUrl AS PostImageUrl, p.UserProfileId AS PostUserProfileId,
                     
                  FROM UserProfile up
                       LEFT JOIN Post p ON p.UserProfileId = up.id
                      
              WHERE up.Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    var reader = cmd.ExecuteReader();

                    UserProfile userprofile = null;
                    if (reader.Read())
                    {
                        userprofile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "PostUserProfileId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
                            post = new Post()
                            {
                                Id = id,
                                Title = DbUtils.GetString(reader, "Title"),
                                Caption = DbUtils.GetString(reader, "Caption"),
                                DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),
                                UserProfileId = DbUtils.GetInt(reader, "PostUserProfileId"),
                            },

                        };




                    }

                    reader.Close();

                    return userprofile;
                }
            }
        }






    }
}