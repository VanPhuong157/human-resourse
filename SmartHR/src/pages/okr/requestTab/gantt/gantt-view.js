// "use client"

// import { useState, useRef } from "react"
// import { Badge } from "@/components/ui/badge"
// import { ChevronRight, ChevronDown } from "lucide-react"
// import type { ProjectTask } from "@/types/project"

// interface GanttViewProps {
//   tasks: ProjectTask[]
//   onTaskUpdate: (taskId: string, field: string, value: any) => void
// }

// interface GanttBar {
//   task: ProjectTask
//   startX: number
//   width: number
//   y: number
// }

// export function GanttView({ tasks, onTaskUpdate }: GanttViewProps) {
//   const [expandedGroups, setExpandedGroups] = useState<Set<string>>(
//     new Set(tasks.filter((t) => t.isGroup).map((t) => t.id)),
//   )
//   const [draggedTask, setDraggedTask] = useState<string | null>(null)
//   const canvasRef = useRef<HTMLDivElement>(null)

//   // Parse date from DD/MM format
//   const parseDate = (dateStr: string | null): Date | null => {
//     if (!dateStr) return null
//     const [day, month] = dateStr.split("/").map(Number)
//     return new Date(2024, month - 1, day)
//   }

//   // Calculate date range for the chart
//   const allDates = tasks
//     .filter((t) => !t.isGroup && t.batDau && t.ketThuc)
//     .flatMap((t) => [parseDate(t.batDau), parseDate(t.ketThuc)])
//     .filter((d): d is Date => d !== null)

//   const minDate = allDates.length > 0 ? new Date(Math.min(...allDates.map((d) => d.getTime()))) : new Date()
//   const maxDate = allDates.length > 0 ? new Date(Math.max(...allDates.map((d) => d.getTime()))) : new Date()

//   // Add padding to date range
//   minDate.setDate(minDate.getDate() - 5)
//   maxDate.setDate(maxDate.getDate() + 5)

//   const totalDays = Math.ceil((maxDate.getTime() - minDate.getTime()) / (1000 * 60 * 60 * 24))
//   const dayWidth = 40 // pixels per day

//   // Generate visible tasks
//   const visibleTasks = tasks.filter((task) => {
//     if (task.isGroup) return true
//     if (task.parentId && !expandedGroups.has(task.parentId)) return false
//     return true
//   })

//   const toggleGroup = (groupId: string) => {
//     setExpandedGroups((prev) => {
//       const next = new Set(prev)
//       if (next.has(groupId)) {
//         next.delete(groupId)
//       } else {
//         next.add(groupId)
//       }
//       return next
//     })
//   }

//   // Calculate bar positions
//   const ganttBars: GanttBar[] = []
//   let yOffset = 0

//   visibleTasks.forEach((task, index) => {
//     if (!task.isGroup && task.batDau && task.ketThuc) {
//       const startDate = parseDate(task.batDau)
//       const endDate = parseDate(task.ketThuc)

//       if (startDate && endDate) {
//         const startDays = Math.floor((startDate.getTime() - minDate.getTime()) / (1000 * 60 * 60 * 24))
//         const duration = Math.ceil((endDate.getTime() - startDate.getTime()) / (1000 * 60 * 60 * 24))

//         ganttBars.push({
//           task,
//           startX: startDays * dayWidth,
//           width: duration * dayWidth,
//           y: yOffset,
//         })
//       }
//     }
//     yOffset += task.isGroup ? 50 : 40
//   })

//   // Calculate progress percentage
//   const getProgress = (task: ProjectTask) => {
//     if (!task.soNgay || !task.thucTe) return 0
//     return Math.min((task.thucTe / task.soNgay) * 100, 100)
//   }

//   // Check if task is on critical path (simplified: tasks with dependencies)
//   const isCriticalPath = (task: ProjectTask) => {
//     return task.dependencies && task.dependencies.length > 0
//   }

//   return (
//     <div className="flex h-full overflow-hidden bg-background">
//       {/* Left sidebar - Task list */}
//       <div className="w-80 border-r bg-card overflow-y-auto">
//         <div className="sticky top-0 bg-card border-b p-3 font-semibold">Danh sách công việc</div>
//         <div>
//           {visibleTasks.map((task, index) => (
//             <div
//               key={task.id}
//               className={`flex items-center gap-2 px-3 py-2 border-b hover:bg-muted/50 ${
//                 task.isGroup ? "bg-yellow-50 dark:bg-yellow-950/20 font-semibold" : ""
//               }`}
//               style={{ height: task.isGroup ? 50 : 40 }}
//             >
//               {task.isGroup ? (
//                 <button onClick={() => toggleGroup(task.id)} className="hover:bg-muted rounded p-0.5">
//                   {expandedGroups.has(task.id) ? (
//                     <ChevronDown className="h-4 w-4" />
//                   ) : (
//                     <ChevronRight className="h-4 w-4" />
//                   )}
//                 </button>
//               ) : (
//                 <div className="w-5" />
//               )}
//               <div className="flex-1 min-w-0">
//                 <div className="truncate text-sm">{task.noiDung}</div>
//                 {!task.isGroup && (
//                   <div className="flex gap-2 text-xs text-muted-foreground mt-0.5">
//                     <span>
//                       {task.batDau} - {task.ketThuc}
//                     </span>
//                     {isCriticalPath(task) && (
//                       <Badge variant="destructive" className="h-4 text-xs">
//                         Critical
//                       </Badge>
//                     )}
//                   </div>
//                 )}
//               </div>
//             </div>
//           ))}
//         </div>
//       </div>

//       {/* Right side - Gantt chart */}
//       <div className="flex-1 overflow-auto">
//         <div className="relative" style={{ minWidth: totalDays * dayWidth + 100 }}>
//           {/* Timeline header */}
//           <div className="sticky top-0 z-10 bg-card border-b">
//             <div className="flex h-12 items-center">
//               {Array.from({ length: totalDays }, (_, i) => {
//                 const date = new Date(minDate)
//                 date.setDate(date.getDate() + i)
//                 return (
//                   <div key={i} className="border-r text-center text-xs" style={{ width: dayWidth, minWidth: dayWidth }}>
//                     <div className="font-semibold">{date.getDate()}</div>
//                     <div className="text-muted-foreground">T{date.getMonth() + 1}</div>
//                   </div>
//                 )
//               })}
//             </div>
//           </div>

//           {/* Gantt bars */}
//           <div className="relative" ref={canvasRef}>
//             {/* Grid lines */}
//             <div className="absolute inset-0 flex">
//               {Array.from({ length: totalDays }, (_, i) => (
//                 <div key={i} className="border-r border-muted" style={{ width: dayWidth, minWidth: dayWidth }} />
//               ))}
//             </div>

//             {/* Task rows background */}
//             {visibleTasks.map((task, index) => (
//               <div
//                 key={task.id}
//                 className={`border-b ${task.isGroup ? "bg-yellow-50/50 dark:bg-yellow-950/10" : ""}`}
//                 style={{ height: task.isGroup ? 50 : 40 }}
//               />
//             ))}

//             {/* Gantt bars */}
//             {ganttBars.map(({ task, startX, width, y }) => {
//               const progress = getProgress(task)
//               const isCritical = isCriticalPath(task)

//               return (
//                 <div key={task.id} className="absolute" style={{ top: y + 8, left: startX, width }}>
//                   {/* Dependencies lines */}
//                   {task.dependencies?.map((depId) => {
//                     const depBar = ganttBars.find((b) => b.task.id === depId)
//                     if (!depBar) return null

//                     const x1 = depBar.startX + depBar.width - startX
//                     const y1 = depBar.y - y

//                     return (
//                       <svg
//                         key={depId}
//                         className="absolute pointer-events-none"
//                         style={{
//                           left: 0,
//                           top: 0,
//                           width: Math.abs(x1) + 20,
//                           height: Math.abs(y1) + 40,
//                           transform: y1 < 0 ? `translateY(${y1}px)` : "none",
//                         }}
//                       >
//                         <path
//                           d={`M ${x1} ${y1 < 0 ? Math.abs(y1) : 0} L ${x1} ${y1 < 0 ? Math.abs(y1) + 10 : 10} L 0 ${y1 < 0 ? Math.abs(y1) + 10 : Math.abs(y1) - 10} L 0 ${y1 < 0 ? Math.abs(y1) + 20 : Math.abs(y1)}`}
//                           stroke={isCritical ? "hsl(var(--destructive))" : "hsl(var(--muted-foreground))"}
//                           strokeWidth="2"
//                           fill="none"
//                           markerEnd="url(#arrowhead)"
//                         />
//                       </svg>
//                     )
//                   })}

//                   {/* Task bar */}
//                   <div
//                     className={`relative h-6 rounded cursor-move transition-all hover:opacity-80 ${
//                       isCritical ? "bg-destructive" : "bg-primary"
//                     }`}
//                     draggable
//                     onDragStart={() => setDraggedTask(task.id)}
//                     onDragEnd={() => setDraggedTask(null)}
//                   >
//                     {/* Progress bar */}
//                     <div
//                       className="absolute inset-0 bg-primary-foreground/30 rounded"
//                       style={{ width: `${progress}%` }}
//                     />

//                     {/* Task label */}
//                     <div className="absolute inset-0 flex items-center px-2 text-xs font-medium text-primary-foreground truncate">
//                       {task.noiDung}
//                     </div>

//                     {/* Resize handles */}
//                     <div className="absolute left-0 top-0 bottom-0 w-1 cursor-ew-resize hover:bg-primary-foreground/50" />
//                     <div className="absolute right-0 top-0 bottom-0 w-1 cursor-ew-resize hover:bg-primary-foreground/50" />
//                   </div>

//                   {/* Task info tooltip */}
//                   <div className="absolute left-0 top-8 hidden group-hover:block bg-popover border rounded-md p-2 text-xs shadow-lg z-20 whitespace-nowrap">
//                     <div className="font-semibold">{task.noiDung}</div>
//                     <div className="text-muted-foreground">
//                       {task.batDau} - {task.ketThuc} ({task.soNgay} ngày)
//                     </div>
//                     <div>Tiến độ: {progress.toFixed(0)}%</div>
//                   </div>
//                 </div>
//               )
//             })}

//             {/* Arrow marker for dependencies */}
//             <svg width="0" height="0">
//               <defs>
//                 <marker id="arrowhead" markerWidth="10" markerHeight="10" refX="9" refY="3" orient="auto">
//                   <polygon points="0 0, 10 3, 0 6" fill="hsl(var(--muted-foreground))" />
//                 </marker>
//               </defs>
//             </svg>
//           </div>
//         </div>
//       </div>
//     </div>
//   )
// }
