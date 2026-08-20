import pandas as pd
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.linear_model import LogisticRegression
import pickle
import os

data = {
    "text": [
        "Ürün harika, kargo çok hızlıydı çok beğendim.", 
        "Kargo çok yavaştı, berbat bir deneyim.", 
        "Fiyatına göre normal bir ürün, idare eder.", 
        "Kesinlikle tavsiye ederim, muazzam bir kalite.", 
        "Param boşa gitti, sakın almayın rezalet."
    ],
    "sentiment": ["positive", "negative", "neutral", "positive", "negative"]
}
df = pd.DataFrame(data)
vectorizer = TfidfVectorizer()
X = vectorizer.fit_transform(df["text"])
y = df["sentiment"]
model = LogisticRegression()
model.fit(X, y)
os.makedirs("models", exist_ok=True)

with open("models/sentiment_model.pkl", "wb") as f:
    pickle.dump(model, f)
    
with open("models/vectorizer.pkl", "wb") as f:
    pickle.dump(vectorizer, f)

print("Tamamm! Model başarıyla eğitildi ve 'models' klasörüne kaydedildi.")