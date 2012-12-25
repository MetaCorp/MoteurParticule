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
        
        public bool D3 { get; set; }

        public Vector2 Vent { get; set; }
        public Vector2 Gravite { get; set; }

        public float VitesseMin { get; set; }
        public float VitesseMax { get; set; }
        public float VariationVitesse { get; set; }

        public float VariationProfondeur { get; set; }

        public float AngleMin { get; set; }
        public float AngleMax { get; set; }

        public Vector2 VariationAngle { get; set; }

        public float SizeMin { get; set; }
        public float SizeMax { get; set; }
        public float VariationSize { get; set; }

        List<Particule> Particules { get; set; }

        public int TTLMin { get; set; }
        public int TTLMax { get; set; }
        public int VariationTTL { get; set; }

        public int NombreGeneration { get; set; }

        public Vector2 variationVent;

        Random random = new Random();

        public MoteurParticule(List<Texture2D> textures, Vector2 position, Vector2 vent, Vector2 gravite, int nbGeneration, bool d3)
        {
            Textures = textures;
            Position = position;
            Vent = vent;

            D3 = d3;

            NombreGeneration = nbGeneration;

            Particules = new List<Particule>();

            Gravite = gravite;
            variationVent = new Vector2(0, 0);
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

        public void setTTL(int TTLMin, int TTLMax, int variationTTL)
        {
            this.TTLMin = TTLMin;
            this.TTLMax = TTLMax;
            VariationTTL = variationTTL;
        }

        private Particule GenererNouvelleParticule(Vector2 position, float angleMin, float angleMax, int TTLMin, int TTLMax, int generation)
        {
            Texture2D texture = Textures[random.Next(Textures.Count)];

            float angle = (float)(angleMin + random.NextDouble() * angleMax);

            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));//new Vector2(0 + (float)(random.NextDouble() - 0.5) * 5f, 0 + (float)(random.NextDouble() - 0.5) * 5f);
            direction.Normalize();

            float vitesse = (float)(VitesseMin + random.NextDouble() * VitesseMax);

            int TTL = random.Next(TTLMin, TTLMax);

            float size = random.Next((int)SizeMin, (int)SizeMax);

            Color Color = new Color(random.Next(255), random.Next(255), random.Next(255));
            float VariationProfondeur;

            if (D3)
                VariationProfondeur = (float)random.NextDouble() - 0.5f;
            else
                VariationProfondeur = 0;

            return new Particule(texture, position, direction, vitesse, TTL, size, VariationProfondeur, generation, Color.White);
        }

        public void GenererParticule(Vector2 position, int totale, float angleMin, float angleMax, int TTLMin, int TTLMax, int generation)
        {
            for (int i = 0; i < totale; i++)
                Particules.Add(GenererNouvelleParticule(position, angleMin, angleMax, TTLMin, TTLMax, generation));
        }

        public void GenererParticule(int totale)
        {
            for (int i = 0; i < totale; i++)
                Particules.Add(GenererNouvelleParticule(Position, AngleMin, AngleMax, TTLMin, TTLMax, 0));
        }

        public void Update(Vector2 position)
        {
            Position = position;

            Vent += variationVent;

            for (int particule = 0; particule < Particules.Count; particule++)
			{
                Particules[particule].Update(Gravite, Vent, VariationVitesse, VariationSize);

                if (Particules[particule].TTL <= 0)
                {
                    if (Particules[particule].Generation < NombreGeneration)
                        GenererParticule(Particules[particule].Position, 5, (float)(-0 * Math.PI / 3), (float)(-6 * Math.PI / 3), 40, 60, Particules[particule].Generation + 1);
                    Particules.RemoveAt(particule);
                }
			}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particule particule in Particules)
                particule.Draw(spriteBatch);
        }

    }
}
