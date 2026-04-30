# MoniChromaticTranscendencePathCalculator

Calculates the shortest or lowest-cost crafting path for the "Chromatic Transcendence" recipe in the Minecraft modpack [Monifactory](https://github.com/ThePansmith/Monifactory).

It supports calculating crafting execution plans for single or multiple machines. However, the current solver uses a brute-force search method without pruning to find best execution plan for multiple machines. The time complexity is $O(12^n\cdot n^m)$, where $n$ is the number of machines and $m$ is the length of the execution plan.

## Chroma

This machine has 12 chromas. The recipe requires a specific chroma and will change the current chroma.

| Name    | Type      |
| ------- | --------- |
| Red     | Primary   |
| Orange  | Other     |
| Yellow  | Secondary |
| Lime    | Other     |
| Green   | Primary   |
| Teal    | Other     |
| Cyan    | Secondary |
| Azure   | Other     |
| Blue    | Primary   |
| Indigo  | Other     |
| Magenta | Secondary |
| Pink    | Other     |

```
         .- Red  -.
    Pink            Orange
    /                 \
Magenta               Yellow
  |                     |
Indigo                Lime
  |                     |
Blue                  Green
    \                 /
    Azure           Teal
         '- Cyan -'
```

## Chromatic Stabilizer

If the current chroma is a primary chroma, subtract 2 units. If the current chroma is a secondary chroma, add 2 units. Otherwise, change the current chroma to the nearest primary chroma.

Consumed upon use.

## Chromatic Capacitor

Stores the primary and secondary chroma values. If the current chroma differs from the chroma value in the provided capacitor by 2 units, shift the current chroma 1 unit towards the chroma value in the capacitor.

Upon use, it returns an empty Chroma Capacitor; therefore, when paired with an appropriate recharging scheme, its operational cost is virtually zero.

## Prismatic Core

Item used in Chromatic Transcendence. Each recipe produces the next Prismatic Core in sequence. Required products are Active Prismatic Cores and Supercritical Prismatic Cores.

| Order | Name          | Required Chroma | Next Chroma |
| ----- | ------------- | --------------- | ----------- |
| 1     | Inert         | Red             | Green       |
| 2     | Red           | Yellow          | Red         |
| 3     | Yellow        | Green           | Blue        |
| 4     | Green         | Cyan            | Green       |
| 5     | Cyan          | Blue            | Red         |
| 6     | Blue          | Magenta         | Blue        |
| 7     | Active        | Orange          | Green       |
| 8     | Orange        | Lime            | Indigo      |
| 9     | Lime          | Teal            | Blue        |
| 10    | Teal          | Azure           | Orange      |
| 11    | Azure         | Indigo          | Red         |
| 12    | Indigo        | Pink            | Teal        |
| 13    | Supercritical | -               | -           |
