using System.Numerics;
using System;
using System.Collections.Generic;
using X975.Radar.Utility;
using X975.Radar.GameObjects.Players;
using X975.Protocol.Connect.Messages.ResponseObj;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;

namespace X975.Radar.GameObjects.Mobs
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class MobsHandler
    {
        public ConcurrentDictionary<int, Mob> mobsList = new ConcurrentDictionary<int, Mob>();

        private readonly List<MobInfo> mobInfos;

        public MobsHandler(List<MobInfo> mobInfos)
        {
            this.mobInfos = mobInfos;
        }

        public void AddMob(int id, int typeId, Vector2 position, Health health, byte enchLvl)
        {
            lock (mobsList)
            {
                if (mobsList.ContainsKey(id))
                    mobsList.TryRemove(id, out Mob m);

                mobsList.TryAdd(id, new Mob(id, typeId, position, enchLvl, mobInfos.Find(x => x.Id == typeId), health));
            } 
        }

        public void UpdateMobPosition(int id, byte[] positionBytes, byte[] newPositionBytes, float speed, DateTime time)
        {
            var position = new Vector2(BitConverter.ToSingle(positionBytes, 4), BitConverter.ToSingle(positionBytes, 0));
            var newPosition = new Vector2(BitConverter.ToSingle(newPositionBytes, 4), BitConverter.ToSingle(newPositionBytes, 0));
            
            lock (mobsList)
            {
                if (mobsList.TryGetValue(id, out Mob mob))
                {
                    mob.Position = position;
                    mob.Speed = speed;
                    mob.Time = time;
                    mob.NewPosition = newPosition;
                }
            } 
        }

        public void SyncMobsPositions()
        {
            lock (mobsList)
            {
                foreach (Mob p in mobsList.Values.ToList())
                {
                    if (p == null || p.Speed == 0) continue;

                    Vector2 posDiff = p.Position - p.NewPosition;

                    if (posDiff == Vector2.Zero) continue;

                    p.Position -= posDiff * (float)((DateTime.UtcNow - p.Time).TotalSeconds / (posDiff.Magnitude() / (p.Speed / 10)));
                }
            }
        }

        public void Remove(int id)
        {
            lock (mobsList)
                mobsList.TryRemove(id, out Mob m);
        }

        public void Clear()
        {
            lock (mobsList)
                mobsList.Clear();
        }

        public void UpdateMobCharge(int mobId, int charge)
        {
            lock (mobsList)
            {
                if (mobsList.TryGetValue(mobId, out Mob mob))
                {
                    mob.Charge = charge;
                }
            }
                
        }

        public void UpdateHealth(int id, int health)
        {
            lock (mobsList)
            {
                if (mobsList.TryGetValue(id, out Mob mob))
                {
                    mob.Health.Value = health;
                }
            }
        }
    }
}
