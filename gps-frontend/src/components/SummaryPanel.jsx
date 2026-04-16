export default function SummaryPanel({ summary }) {
  if (!summary) return null;

  return (
    <div style={{ padding: 10 }}>
      <h3>Session Summary</h3>

      <p>Total Distance: {summary.totalDistanceKm?.toFixed(2)} km</p>
      <p>Avg Speed: {summary.avgSpeed?.toFixed(2)}</p>
      <p>Max Speed: {summary.maxSpeed?.toFixed(2)}</p>
      <p>Max Heart Rate: {summary.maxHeartRate}</p>
      <p>Samples: {summary.sampleCount}</p>
      <p>Duration: {summary.durationSeconds}s</p>
    </div>
  );
}
