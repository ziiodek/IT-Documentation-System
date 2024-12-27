using System.DirectoryServices;


namespace ITDocumentation
{
    public class ActiveDirectoryCon
    {
        string DOMAIN = "DOMAIN";
        string EMAIL_DOMAIN = "@firstlightfcu.org";


        public List<string> getAllDepartments()
        {
            List<string> departments = new List<string>();
            DirectorySearcher ds = new DirectorySearcher();
            DirectoryEntry de = new DirectoryEntry("LDAP://" + DOMAIN);
            SearchResultCollection sr;


            // Build User Searcher
            ds = new DirectorySearcher(de);
            //PRODUCTION QUERY
            //ds.Filter =  "(&(objectCategory=User)(objectClass=organizationalPerson)(objectClass=person)(memberof=ITDocumentation)(userprincipalname=" +userName+ "))";

            //DEVELOPMENT QUERY
            ds.Filter = "(&(objectClass=user))";
            ds.PropertiesToLoad.Add("department");


            sr = ds.FindAll();

            if (sr.Count > 0)
            {
                foreach (SearchResult srResult in sr)
                {
                    if (srResult.Properties.Contains("department"))
                    {
                        departments.Add(srResult.Properties["department"][0].ToString()!.ToUpper());
                    }

                }
                return departments;
            }

            sr.Dispose();
            return new List<string>();
        }


        public List<User> getAllUsers(string department)
        {
            List<User> users = null;
            DirectorySearcher ds = null;
            DirectoryEntry de = new DirectoryEntry("LDAP://" + DOMAIN);
            SearchResultCollection sr;


            // Build User Searcher
            ds = new DirectorySearcher(de);
            //PRODUCTION QUERY
            //ds.Filter =  "(&(objectCategory=User)(objectClass=organizationalPerson)(objectClass=person)(memberof=ITDocumentation)(userprincipalname=" +userName+ "))";

            //DEVELOPMENT QUERY
            ds.Filter = "(&(objectCategory=User)(objectClass=organizationalPerson)(objectClass=person)(department=" + department + "))";
            ds.PropertiesToLoad.Add("department");
            ds.PropertiesToLoad.Add("displayname");
            ds.PropertiesToLoad.Add("userprincipalname");
            ds.PropertiesToLoad.Add("userAccountControl");
            sr = ds.FindAll();

            if (sr.Count > 0)
            {
                users = new List<User>();

                foreach (SearchResult srResult in sr)
                {
                    srResult.ToString();
                    if (srResult.Properties.Contains("displayname")
                    && srResult.Properties.Contains("department")
                    && srResult.Properties.Contains("userprincipalname"))
                    {
                        int enableValue = (int)srResult.Properties["userAccountControl"][0];
                        if (enableValue != 514)
                        {
                            User user = new User();
                            user.Name = srResult.Properties["displayname"][0].ToString();
                            user.Department = srResult.Properties["department"][0].ToString();
                            string username = srResult.Properties["userprincipalname"][0].ToString().Replace(EMAIL_DOMAIN, "");
                            user.Username = username;
                            users.Add(user);
                        }

                    }

                }

            }

            sr.Dispose();
            return users;
        }



        public User GetAUser(string userName)
        {
            DirectorySearcher ds = null;
            DirectoryEntry de = new DirectoryEntry("LDAP://" + DOMAIN);
            SearchResult sr;
            userName = userName + EMAIL_DOMAIN;

            // Build User Searcher
            ds = new DirectorySearcher(de);
            //PRODUCTION QUERY
            //ds.Filter =  "(&(objectCategory=User)(objectClass=organizationalPerson)(objectClass=person)(memberof=ITDocumentation)(userprincipalname=" +userName+ "))";

            //DEVELOPMENT QUERY
            ds.Filter = "(&(objectCategory=User)(objectClass=organizationalPerson)(objectClass=person)(userprincipalname=" + userName + "))";
            ds.PropertiesToLoad.Add("department");
            ds.PropertiesToLoad.Add("displayname");


            sr = ds.FindOne();

            if (sr != null)
            {
                if (sr.Properties.Contains("displayname") && sr.Properties.Contains("department"))
                {
                    User user = new User();
                    user.Name = sr.Properties["displayname"][0].ToString();
                    user.Department = sr.Properties["department"][0].ToString();

                    return user;
                }


            }

            // Return a blank user to avoid random null.
            return new User();
        }




        public bool AuthenticateUser(string userName, string password)
        {
            bool ret = false;

            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://" + DOMAIN, userName, password);
                DirectorySearcher dsearch = new DirectorySearcher(de);
                SearchResult results = null;

                results = dsearch.FindOne();

                ret = true;
            }
            catch
            {
                ret = false;
            }

            return ret;
        }





    }

}