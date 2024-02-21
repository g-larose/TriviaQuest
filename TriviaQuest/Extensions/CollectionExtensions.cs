using System.Collections;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using TriviaQuest.Helpers;

namespace TriviaQuest.Extensions
{
    public static class CollectionExtensions
    {
        private static Random random = new();

        /// <summary>Returns the index of an element contained in a list if it is found, otherwise returns -1.</summary>
        public static int IndexOf<T>(this IReadOnlyList<T> list, T element) // IList doesn't implement IndexOf for some reason
        {
            for (var i = 0; i < list.Count; i++)
                if (list[i]?.Equals(element) ?? false)
                    return i;
            return -1;
        }

        /// <summary>Fluid method that joins the members of a collection using the specified separator between them.</summary>
        public static string Join<T>(this IEnumerable<T> values, string separator = "")
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// Extension method to shuffle an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>Shuffled Array</returns>
        public static T[] Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int r = random.Next(n + 1);
                T t = array[r];
                array[r] = array[n];
                array[n] = t;
            }

            return array;
        }

        /// <summary>
        /// Converts an object into a JSON string
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ToJson(this object item)
        {
            var ser = new DataContractJsonSerializer(item.GetType());
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, item);
                var sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }
        public static string ToJson(this IEnumerable collection, string rootName)
        {
            var ser = new DataContractJsonSerializer(collection.GetType());
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, collection);
                var sb = new StringBuilder();
                sb.Append("{ \"").Append(rootName).Append("\": ");
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                sb.Append(" }");
                return sb.ToString();
            }
        }
        /// <summary>
        /// Converts a JSON string into the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns>The converted object</returns>
        public static T FromJsonTo<T>(this string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                T jsonObject = (T)ser.ReadObject(ms);
                return jsonObject;
            }
        }

        public static string FromCatToNumber(this string cat)
        {
            cat = cat.ToLower();
            var result = cat switch
            {
                "art" => "25",
                "general knowledge" => "9",
                "books" => "10",
                "film" => "11",
                "music" => "12",
                "theatre" => "13",
                "tv" => "14",
                "video games" => "15",
                "board games" => "16",
                "science" => ShuffleHelper.ShuffleScienceCategory(new string[] { "nature", "computers", "mathmatics", "gadgets" }),
                "mythology" => "20",
                "sports" => "21",
                "geography" => "22",
                "history" => "23",
                "politics" => "24",
                "celebrities" => "26",
                "animals" => "27",
                "entertainment" => ShuffleHelper.ShuffleEntertainmentCategory(new string[] { "comics", "anime", "cartoons" }),
                _ => "9"
            };
            return result;
        }

        public static string Sanitize(this string input)
        {
            var replacements = new Dictionary<string, string>
            {
                { "&quot;", "'" },
                { "&#039;", "'" },
                { "&hellip;", "'" },
                { "&rdque;", "'" },
                { "&rsquo;", "'" },
                { "&amp;", "'" },
                { "&ldquo;", "'" }
            };

            string result = Regex.Replace(input,
                                       string.Join("|", replacements.Keys),
                                       match => replacements[match.Value]);

            return result;
        }
    }
}
