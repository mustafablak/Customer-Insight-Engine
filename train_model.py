import pandas as pd
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.linear_model import LogisticRegression
import pickle
import os

print("Veri setleri yükleniyor...")
df_en = pd.read_csv("english_dataset.csv")
df_tr = pd.read_csv("turkish_dataset.csv")
df_en = df_en.rename(columns={"review": "text", "sentiment": "sentiment"})
df_en = df_en.dropna(subset=["text", "sentiment"])
df_en = df_en[df_en["sentiment"].isin(["positive", "negative"])]
df_tr = df_tr.rename(columns={"combined_text": "text", "label": "sentiment"})
df_tr = df_tr.dropna(subset=["text", "sentiment"])
df_tr["sentiment"] = df_tr["sentiment"].replace({0: "negative", 1: "positive", "0": "negative", "1": "positive", 0.0: "negative", 1.0: "positive"})
df_tr = df_tr[df_tr["sentiment"].isin(["positive", "negative"])]

print("Veri setleri birleştiriliyor...")
df_combined = pd.concat([df_en, df_tr], ignore_index=True)
df_combined = df_combined.sample(frac=1).reset_index(drop=True)

print(f"Toplam {len(df_combined)} temiz satır ile eğitim başlıyor...")
vectorizer = TfidfVectorizer(max_features=10000, stop_words='english')
X = vectorizer.fit_transform(df_combined["text"].values.astype('U'))
y = df_combined["sentiment"].astype(str) # Etiketlerin metin olduğundan kesin olarak emin oluyoruz

print("Model eğitiliyor, lütfen bekleyin...")
model = LogisticRegression(max_iter=1000)
model.fit(X, y)
os.makedirs("models", exist_ok=True)

with open("models/sentiment_model.pkl", "wb") as f:
    pickle.dump(model, f)
    
with open("models/vectorizer.pkl", "wb") as f:
    pickle.dump(vectorizer, f)

print("Harika! Çok dilli model başarıyla eğitildi ve kaydedildi.")