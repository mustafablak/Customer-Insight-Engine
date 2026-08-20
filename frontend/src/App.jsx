import { useState } from 'react'
import './App.css'

function App() {
  const [review, setReview] = useState('')
  const [result, setResult] = useState(null)
  const [loading, setLoading] = useState(false)

  const handleAnalyze = async () => {
    if (!review) return;
    setLoading(true);
    
    try {
      const response = await fetch('http://localhost:5111/api/Insight/analyze', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': '*/*'
        },
        body: JSON.stringify(review)
      });
      
      const data = await response.json();
      setResult(data);
    } catch (error) {
      console.error("Analiz sırasında hata oluştu:", error);
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className="container">
      <h1>Müşteri İçgörü Motoru</h1>
      <p className="subtitle">Yapay Zeka Destekli Duygu Analizi</p>

      <div className="input-section">
        <textarea 
          placeholder="Analiz edilecek müşteri yorumunu buraya yazın..."
          value={review}
          onChange={(e) => setReview(e.target.value)}
          rows="4"
        />
        <button onClick={handleAnalyze} disabled={loading}>
          {loading ? 'Analiz Ediliyor...' : 'Yorumu Analiz Et'}
        </button>
      </div>

      {result && (
        <div className={`result-card ${result.sentiment}`}>
          <h2>Sonuç: {result.sentiment === 'positive' ? 'Olumlu 👍' : 'Olumsuz 👎'}</h2>
          
          <div className="confidence-section">
            <p>Yapay Zeka Doğruluk Oranı (Güven Skoru): <strong>%{Math.round(result.confidence * 100)}</strong></p>
            <div className="progress-bar-bg">
              <div 
                className="progress-bar-fill" 
                style={{ width: `${result.confidence * 100}%` }}
              ></div>
            </div>
          </div>
          <p className="db-info">Veritabanı Kayıt ID: #{result.id}</p>
        </div>
      )}
    </div>
  )
}

export default App