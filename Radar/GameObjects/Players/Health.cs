using System.Reflection;

namespace X975.Radar.GameObjects.Players
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class Health
    {
        public Health(int maxValue)
        {
            MaxValue = maxValue;
            Value = maxValue;
            Regeneration = 0f;
            IsRegeneration = false;
        }

        public Health(int value, int maxValue)
        {
            MaxValue = maxValue;
            Value = value;
            Regeneration = 0f;
            IsRegeneration = false;
        }

        public Health(int value, int maxValue, float regeneration)
        {
            MaxValue = maxValue;
            Value = value;
            Regeneration = regeneration;
            IsRegeneration = true;
        }

        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value >= MaxValue)
                {
                    this.value = MaxValue;
                    IsRegeneration = false;
                }
                else
                {
                    this.value = value;
                }
            }
        }

        private int value;

        public int MaxValue { get; set; }

        public float Regeneration { get; set; }

        public bool IsRegeneration { get; set; }

        public string strPercent
        {
            get
            {
                return Percent.ToString() + "%";
            }
        }

        public int Percent
        {
            get
            {
                if (value * 100 == 0)
                    return 0;

                return (value * 100 / MaxValue);
            }
        }
    }
}
