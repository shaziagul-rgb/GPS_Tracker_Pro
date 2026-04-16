import React, { memo } from "react";
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer
} from "recharts";

function ChartPanel({ chart = [] }) {
  return (
    <div style={{ height: 250 }}>
      <h3>Performance Chart</h3>

      <ResponsiveContainer width="100%" height="100%">
        <LineChart data={chart}>
          <XAxis dataKey="index" />
          <YAxis />
          <Tooltip />

          <Line type="monotone" dataKey="sog" stroke="blue" dot={false} />
          <Line type="monotone" dataKey="vmg" stroke="green" dot={false} />
        </LineChart>
      </ResponsiveContainer>
    </div>
  );
}

export default memo(ChartPanel);