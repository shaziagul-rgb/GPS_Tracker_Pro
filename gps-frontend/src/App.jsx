import { useEffect, useState, useRef } from "react";
import MapView from "./components/MapView";
import StatsPanel from "./components/StatsPanel";
import ChartPanel from "./components/ChartPanel";
import SummaryPanel from "./components/SummaryPanel";
import { getGpsData } from "./apis/gpsApi";

export default function App() {
 
  const [track, setTrack] = useState([]);
  const [stats, setStats] = useState(null);
  const [summary, setSummary] = useState(null);
  const [chart, setChart] = useState([]);

  
  const [loading, setLoading] = useState(true);
  const [chartLoading, setChartLoading] = useState(true);

  const [playing, setPlaying] = useState(false);
  const [currentIndex, setCurrentIndex] = useState(0);

  const intervalRef = useRef(null);

  
  useEffect(() => {
    const controller = new AbortController();

    async function load() {
      try {
        const data = await getGpsData(controller.signal);

        
        setTrack(data.track || []);
        setStats(data.stats || null);

       
        //setSummary(data.summary || null);

       
        requestAnimationFrame(() => {
          setChart(data.chart || []);
          setChartLoading(false);
        });

      } catch (err) {
        if (err.name !== "CanceledError") {
          console.error(err);
        }
      } finally {
        setLoading(false);
      }
    }

    load();
    return () => controller.abort();
  }, []);

  
  useEffect(() => {
    if (!playing || track.length === 0) return;

    intervalRef.current = setInterval(() => {
      setCurrentIndex((prev) => {
        if (prev >= track.length - 1) {
          setPlaying(false);
          return prev;
        }
        return prev + 1;
      });
    }, 200); // smoother + less CPU load

    return () => clearInterval(intervalRef.current);
  }, [playing, track]);

  
  if (loading) {
    return (
      <div style={{ padding: 20 }}>
        Loading GPS session...
      </div>
    );
  }

  // =========================
  // UI
  // =========================
  return (
    <div style={styles.page}>
      
      {/* MAP (ALWAYS FAST) */}
      <div style={styles.map}>
        <MapView track={track} currentIndex={currentIndex} />
      </div>

      {/* RIGHT PANEL */}
      <div style={styles.sidebar}>

        {/* STATS (FAST) */}
        <StatsPanel
          stats={stats}
          track={track}
          currentIndex={currentIndex}
        />

        {/* SUMMARY (FAST) */}
        <SummaryPanel summary={summary} />

        {/* CHART (LAZY / HEAVY) */}
        <div style={{ marginTop: 10 }}>
          {chartLoading ? (
            <div style={styles.loader}>Loading chart...</div>
          ) : (
            <ChartPanel chart={chart} />
          )}
        </div>

      </div>

      {/* CONTROLS */}
      <div style={styles.controls}>
        <button onClick={() => setPlaying(p => !p)}>
          {playing ? "Pause" : "Play"}
        </button>

        <input
          type="range"
          min="0"
          max={track.length - 1 || 0}
          value={currentIndex}
          onChange={(e) => {
            setPlaying(false);
            setCurrentIndex(Number(e.target.value));
          }}
        />
      </div>

    </div>
  );
}

// =========================
// STYLES
// =========================
const styles = {
  page: {
    display: "flex",
    height: "100vh",
    width: "100vw",
    fontFamily: "sans-serif"
  },

  map: {
    flex: 1
  },

  sidebar: {
    width: 350,
    display: "flex",
    flexDirection: "column",
    borderLeft: "1px solid #ddd",
    overflowY: "auto",
    padding: 10
  },

  controls: {
    position: "absolute",
    bottom: 10,
    left: 10,
    background: "#fff",
    padding: 10,
    borderRadius: 8,
    display: "flex",
    gap: 10
  },

  loader: {
    padding: 10,
    background: "#f5f5f5",
    borderRadius: 6
  }
};