using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MoteurParticule
{
    public class MoteurParticule
    {
        public List<Texture2D> Textures { get; set; }
        public Vector2 Position { get; set; }

        public Vector2 Vent { get; set; }
        public Vector2 Gravite { get; set; }

        public float VitesseMin { get; set; }
        public float VitesseMax { get; set; }
        public float VariationVitesse { get; set; }

        public float AngleMin { get; set; }
        public float AngleMax { get; set; }

        public Vector2 VariationAngle { get; set; }

        public float SizeMin { get; set; }
        public float SizeMax { get; set; }
        public float VariationSize { get; set; }

        List<Particule> Particules;

        Random random = new Random();

        public MoteurParticule(List<Texture2D> textures, Vector2 position, Vector2 vent)
        {
            Textures = textures;
            Position = position;
            Vent = vent;

            Particules = new List<Particule>();

            Gravite = new Vector2(0, 0.01f);
        }

        public void setAngle(float angleMin, float angleMax)
        {
            AngleMin = angleMin;
            AngleMax = angleMax;
            //VariationAngle = variationAngle;
        }

        public void setSize(float sizeMin, float sizeMax, float variationSize)
        {
            SizeMin = sizeMin;
            SizeMax = sizeMax;
            VariationSize = variationSize;
        }

        public void setVitesse(float vitesseMin, float vitesseMax, float variationVitesse)
        {
            VitesseMin = vitesseMin;
            VitesseMax = vitesseMax;
            VariationVitesse = variationVitesse;
        }

        private Particule GenererNouvelleParticule()
        {
            Texture2D texture = Textures[random.Next(Textures.Count)];

            float angle = (float)(AngleMin + random.NextDouble() * AngleMax);

            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));//new Vector2(0 + (float)(random.NextDouble() - 0.5) * 5f, 0 + (float)(random.NextDouble() - 0.5) * 5f);
            direction.Normalize();

            float vitesse = (float)(VitesseMin + random.NextDouble() * VitesseMax);

            int TTL = random.Next(20, 60) * 5;

            float size = random.Next((int)SizeMin, (int)SizeMax);

            Color Color = new Color(random.Next(255), random.Next(255), random.Next(255));

            return new Particule(texture, Position, direction, vitesse, TTL, size, Color);
        }

        public void Update()
        {
            int totale = 10;

            for (int i = 0; i < totale; i++)
                Particules.Add(GenererNouvelleParticule());

            for (int particule = 0; particule < Particules.Count; particule++)
			{
                Particules[particule].Update(Gravite, Vent, VariationVitesse, VariationSize);

                if (Particules[particule].TTL == 0)
                    Particules.RemoveAt(particule);
			}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particule particule in Particules)
                particule.Draw(spriteBatch);
        }

    }
}
