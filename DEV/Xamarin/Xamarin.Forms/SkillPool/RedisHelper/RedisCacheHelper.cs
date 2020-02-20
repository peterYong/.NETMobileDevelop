using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace RedisHelper
{
    /// <summary>
    /// redis缓存帮助类【单机】
    /// </summary>
    public class RedisCacheHelper
    {
        private static PooledRedisClientManager pool = null;
        private static string[] redisHosts = null;
        public static int RedisMaxReadPool = 30;
        public static int RedisMaxWritePool = 10;

        public static void Init()
        {
            //default is: localhost:6379 ,可以多个，用逗号隔开
            //带密码的是【查源码可以发现】：password@ip:port
            var redisHostStr = "0511@120.79.67.39:6379";

            if (!string.IsNullOrEmpty(redisHostStr))
            {
                redisHosts = redisHostStr.Split(',');

                if (redisHosts.Length > 0)
                {
                    //PooledRedisClientManager 使用的是客户端链接池模式，这样的存取效率应该是最高的。同时也更方便的支持读写分离，均衡负载。
                    pool = new PooledRedisClientManager(readWriteHosts: redisHosts, readOnlyHosts: redisHosts, config:
                        new RedisClientManagerConfig()
                        {
                            MaxWritePoolSize = RedisMaxWritePool,
                            MaxReadPoolSize = RedisMaxReadPool,
                            AutoStart = true
                        });
                }
            }
        }

        /// <summary>
        /// 增加缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry">过期时间</param>
        public static void Add<T>(string key, T value, DateTime expiry)
        {
            if (value == null)
            {
                return;
            }

            if (expiry <= DateTime.Now)
            {
                Remove(key);
                return;
            }

            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            //r.SendTimeout = 1000;
                            r.Set(key, value, expiry - DateTime.Now);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "存储", key + ex.ToString());
            }

        }

        public static void Add<T>(string key, T value, TimeSpan slidingExpiration)
        {
            if (value == null)
            {
                return;
            }

            if (slidingExpiration.TotalSeconds <= 0)
            {
                Remove(key);

                return;
            }

            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            //r.SendTimeout = 1000;
                            r.Set(key, value, slidingExpiration);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "存储", key + ex.ToString());
            }

        }


        /// <summary>
        /// 根据key获取缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }

            T obj = default(T);

            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            //r.SendTimeout = 1000;
                            obj = r.Get<T>(key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "获取", key + ex.ToString());
            }
            return obj;
        }

        /// <summary>
        /// 根据key的匹配符 获取缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IDictionary<string, T> GetAll<T>(string key) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            //r.SendTimeout = 1000;
                            IEnumerable<string> pattern = r.GetKeysByPattern(key);
                            return r.GetAll<T>(pattern);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "获取", key + ex.ToString());
            }
            return null;
        }


        public static void Remove(string key)
        {
            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            //r.SendTimeout = 1000;
                            r.Remove(key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "删除", key + ex.ToString());
            }
        }

        public static bool Exists(string key)
        {
            try
            {
                if (pool != null)
                {
                    using (var r = pool.GetClient())
                    {
                        if (r != null)
                        {
                            //r.SendTimeout = 1000;
                            return r.ContainsKey(key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:{1}发生异常!{2}", "cache", "是否存在", key + ex.ToString());
            }
            return false;
        }

        #region 发布订阅

        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static long RedisPub(string channel, string msg)
        {
            try
            {
                using (var r = pool.GetClient())
                {
                    if (r != null)
                    {
                        //r.SendTimeout = 1000;
                        return r.PublishMessage(channel, msg);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return 0;
        }


        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="subChannel"></param>
        public static void RedisSub(string subChannel, Action<string, string> action)
        {
            IRedisSubscription subscription = null;
            using (var r = pool.GetClient())
            {
                if (r != null)
                {
                    //r.SendTimeout = 1000;
                    //创建订阅
                    subscription = r.CreateSubscription();
            //    }
            //}
            //接收消息处理Action
            //subscription.OnMessage = (channel, msg) =>
            //{
            //    Console.WriteLine("频道【{0}】[{1}]", channel, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //    Console.WriteLine(msg);
            //};
            subscription.OnMessage = action;

            //订阅事件处理
            subscription.OnSubscribe = (channel) =>
            {
                Console.WriteLine("订阅客户端：开始订阅" + channel);
            };
            //取消订阅事件处理
            subscription.OnUnSubscribe = (a) => { Console.WriteLine("订阅客户端：取消订阅"); };
            //订阅频道
            subscription.SubscribeToChannels(subChannel);
                }
            }
        }

        private static void SubCallBack(string msg)
        {
            Console.WriteLine("Cliect1:{0}", msg);
        }




        #endregion

    }
}
