"use client";

import React, { useState } from "react";
// Icons 
import { 
  CheckCircle2, Clock, AlertCircle, Users, Calendar, 
  Trophy, TrendingUp 
} from "lucide-react";
// Charts
import { 
  BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, 
  ResponsiveContainer, Legend
} from "recharts";

// --- DỮ LIỆU MẪU (Sau này thay thế bằng API call) ---
const employees = [
  { id: 1, name: "Nguyễn Văn A", role: "Developer", completedTasks: 45, totalTasks: 50, performance: "excellent" },
  { id: 2, name: "Trần Thị B", role: "Designer", completedTasks: 38, totalTasks: 45, performance: "good" },
  { id: 3, name: "Lê Văn C", role: "Developer", completedTasks: 42, totalTasks: 50, performance: "excellent" },
  { id: 4, name: "Phạm Thị D", role: "Marketing", completedTasks: 28, totalTasks: 40, performance: "good" },
  { id: 5, name: "Hoàng Văn E", role: "Developer", completedTasks: 25, totalTasks: 50, performance: "average" },
  { id: 6, name: "Võ Thị F", role: "Designer", completedTasks: 18, totalTasks: 45, performance: "average" },
];

const performanceData = [
  { name: "Tuần 1", hoanthanh: 65, danglam: 15, trehan: 5 },
  { name: "Tuần 2", hoanthanh: 72, danglam: 12, trehan: 3 },
  { name: "Tuần 3", hoanthanh: 58, danglam: 20, trehan: 8 },
  { name: "Tuần 4", hoanthanh: 85, danglam: 10, trehan: 2 },
];

// --- CÁC COMPONENT UI NỘI BỘ (Thay thế cho Shadcn/UI) ---

const CustomCard = ({ children, className = "" }) => (
  <div className={`bg-white rounded-xl border border-gray-200 shadow-sm ${className}`}>
    {children}
  </div>
);

const CustomProgressBar = ({ value }) => (
  <div className="w-full bg-gray-100 rounded-full h-2">
    <div 
      className="bg-blue-600 h-2 rounded-full transition-all duration-500" 
      style={{ width: `${value}%` }}
    />
  </div>
);

const CustomBadge = ({ variant, children }) => {
  const styles = {
    excellent: "bg-green-100 text-green-700",
    good: "bg-blue-100 text-blue-700",
    average: "bg-yellow-100 text-yellow-700",
    poor: "bg-red-100 text-red-700",
  };
  return (
    <span className={`px-2 py-1 rounded-md text-[10px] font-bold uppercase ${styles[variant] || "bg-gray-100"}`}>
      {children}
    </span>
  );
};

// --- TRANG DASHBOARD CHÍNH ---
export default function Dashboards() {
  const [timePeriod, setTimePeriod] = useState("Tháng này");

  return (
    <div className="min-h-screen bg-[#f8fafc] pb-12 text-slate-900">
      {/* Header */}
      <header className="bg-white border-b border-gray-200 px-8 py-6 mb-8 sticky top-0 z-10">
        <div className="max-w-7xl mx-auto flex flex-col md:flex-row md:items-center justify-between gap-4">
          <div>
            <h1 className="text-2xl font-extrabold tracking-tight">Dashboard Nhân Sự</h1>
            <p className="text-sm text-slate-500 font-medium">Theo dõi hiệu suất & tiến độ dự án</p>
          </div>
          <div className="flex items-center gap-3">
            <select 
              className="bg-white border border-gray-300 text-sm rounded-lg p-2.5 outline-none focus:ring-2 focus:ring-blue-500"
              value={timePeriod}
              onChange={(e) => setTimePeriod(e.target.value)}
            >
              <option>Tuần này</option>
              <option>Tháng này</option>
              <option>Quý này</option>
            </select>
            <button className="flex items-center gap-2 bg-blue-600 hover:bg-blue-700 text-white px-4 py-2.5 rounded-lg text-sm font-semibold transition-all">
              <Calendar className="w-4 h-4" /> Xuất báo cáo
            </button>
          </div>
        </div>
      </header>

      <main className="max-w-7xl mx-auto px-8 space-y-8">
        {/* 1. Hàng Thống Kê Tổng Quan */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          {[
            { label: "Hoàn thành", val: "280", color: "text-green-600", bg: "bg-green-50", icon: CheckCircle2 },
            { label: "Đang làm", val: "64", color: "text-blue-600", bg: "bg-blue-50", icon: Clock },
            { label: "Trễ hạn", val: "12", color: "text-red-600", bg: "bg-red-50", icon: AlertCircle },
            { label: "Nhân viên", val: "24", color: "text-purple-600", bg: "bg-purple-50", icon: Users },
          ].map((stat, i) => (
            <CustomCard key={i} className="p-6">
              <div className="flex justify-between items-start">
                <div>
                  <p className="text-sm font-medium text-slate-500">{stat.label}</p>
                  <p className="text-3xl font-bold mt-1">{stat.val}</p>
                </div>
                <div className={`p-3 rounded-xl ${stat.bg}`}>
                  <stat.icon className={`w-6 h-6 ${stat.color}`} />
                </div>
              </div>
            </CustomCard>
          ))}
        </div>

        {/* 2. Biểu đồ & Nhân viên nổi bật */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          <CustomCard className="p-6 lg:col-span-2">
            <h3 className="text-lg font-bold mb-6">Tiến độ công việc</h3>
            <div className="h-[300px] w-full">
              <ResponsiveContainer width="100%" height="100%">
                <BarChart data={performanceData}>
                  <CartesianGrid strokeDasharray="3 3" vertical={false} stroke="#f1f5f9" />
                  <XAxis dataKey="name" fontSize={12} tickLine={false} axisLine={false} />
                  <YAxis fontSize={12} tickLine={false} axisLine={false} />
                  <Tooltip cursor={{fill: '#f8fafc'}} />
                  <Legend iconType="circle" />
                  <Bar dataKey="hoanthanh" fill="#10b981" name="Xong" radius={[4, 4, 0, 0]} barSize={30} />
                  <Bar dataKey="danglam" fill="#3b82f6" name="Đang xử lý" radius={[4, 4, 0, 0]} barSize={30} />
                </BarChart>
              </ResponsiveContainer>
            </div>
          </CustomCard>

          <CustomCard className="p-6">
            <h3 className="text-lg font-bold mb-6 flex items-center gap-2">
              <Trophy className="w-5 h-5 text-yellow-500" /> Ngôi sao tháng
            </h3>
            <div className="space-y-4">
              {employees.slice(0, 3).map((emp) => (
                <div key={emp.id} className="flex items-center justify-between p-4 bg-slate-50 rounded-xl border border-slate-100">
                  <div className="flex flex-col">
                    <span className="font-bold text-sm">{emp.name}</span>
                    <span className="text-xs text-slate-500">{emp.role}</span>
                  </div>
                  <div className="text-right">
                    <span className="text-sm font-black text-blue-600">{emp.completedTasks}</span>
                    <p className="text-[10px] text-slate-400 font-bold uppercase">Tasks</p>
                  </div>
                </div>
              ))}
            </div>
          </CustomCard>
        </div>

        {/* 3. Danh sách nhân viên */}
        <div>
          <div className="flex items-center justify-between mb-6">
            <h2 className="text-xl font-extrabold uppercase tracking-wider">Danh sách nhân sự</h2>
          </div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {employees.map((emp) => {
              const rate = Math.round((emp.completedTasks / emp.totalTasks) * 100);
              const performanceLabels = { 
                excellent: "Xuất sắc", 
                good: "Tốt", 
                average: "Trung bình",
                poor: "Cần cố gắng"
              };

              return (
                <CustomCard key={emp.id} className="p-6 hover:border-blue-300 transition-all group">
                  <div className="flex justify-between items-start mb-6">
                    <div>
                      <h4 className="font-bold group-hover:text-blue-600 transition-colors">{emp.name}</h4>
                      <p className="text-xs font-medium text-slate-500 italic mt-0.5">{emp.role}</p>
                    </div>
                    <CustomBadge variant={emp.performance}>
                      {performanceLabels[emp.performance]}
                    </CustomBadge>
                  </div>
                  
                  <div className="space-y-4">
                    <div className="flex justify-between text-xs font-bold">
                      <span className="text-slate-400 uppercase">Hiệu suất</span>
                      <span>{rate}%</span>
                    </div>
                    <CustomProgressBar value={rate} />
                    <div className="flex justify-between items-center pt-2">
                      <div className="text-[10px] font-bold text-slate-400 uppercase">Hoàn thành</div>
                      <div className="text-sm font-black text-slate-700">{emp.completedTasks} / {emp.totalTasks}</div>
                    </div>
                  </div>
                </CustomCard>
              );
            })}
          </div>
        </div>
      </main>
    </div>
  );
}