import pickle
from fastapi import FastAPI
from pydantic import BaseModel

app = FastAPI(title="AI Insight Engine API", version="1.0")
with open("models/sentiment_model.pkl", "rb") as f:
    model = pickle.load(f)

with open("models/vectorizer.pkl", "rb") as f:
    vectorizer = pickle.load(f)

class UserReview(BaseModel):
    text: str

@app.post("/analyze-sentiment/")
async def analyze_sentiment(review: UserReview):
    text_vectorized = vectorizer.transform([review.text])
    prediction = model.predict(text_vectorized)[0]
    probabilities = model.predict_proba(text_vectorized)[0]
    confidence = max(probabilities)
    
    return {
        "review_text": review.text,
        "sentiment": prediction,
        "confidence": round(confidence, 2)
    }