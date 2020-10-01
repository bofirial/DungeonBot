﻿
using System.Collections.Generic;
using DungeonBot.Models;
using Fluxor;

namespace DungeonBot.Client.Store.ActionModule
{
    public class ActionModuleFeature : Feature<ActionModuleState>
    {
        public override string GetName() => nameof(ActionModuleState);

        protected override ActionModuleState GetInitialState()
        {
            return new ActionModuleState(new List<ActionModuleLibrary>()
            {
                new ActionModuleLibrary(
                    "DungeonBot001",
                    //TODO: Pull from file instead?
                    new byte[] {77, 90, 144, 0, 3, 0, 0, 0, 4, 0, 0, 0, 255, 255, 0, 0, 184, 0, 0, 0, 0, 0, 0, 0, 64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 0, 0, 14, 31, 186, 14, 0, 180, 9, 205, 33, 184, 1, 76, 205, 33, 84, 104, 105, 115, 32, 112, 114, 111, 103, 114, 97, 109, 32, 99, 97, 110, 110, 111, 116, 32, 98, 101, 32, 114, 117, 110, 32, 105, 110, 32, 68, 79, 83, 32, 109, 111, 100, 101, 46, 13, 13, 10, 36, 0, 0, 0, 0, 0, 0, 0, 80, 69, 0, 0, 76, 1, 2, 0, 234, 221, 95, 95, 0, 0, 0, 0, 0, 0, 0, 0, 224, 0, 34, 32, 11, 1, 48, 0, 0, 8, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 174, 39, 0, 0, 0, 32, 0, 0, 0, 64, 0, 0, 0, 0, 0, 16, 0, 32, 0, 0, 0, 2, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 96, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 3, 0, 64, 133, 0, 0, 16, 0, 0, 16, 0, 0, 0, 0, 16, 0, 0, 16, 0, 0, 0, 0, 0, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 92, 39, 0, 0, 79, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 32, 0, 0, 72, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 46, 116, 101, 120, 116, 0, 0, 0, 180, 7, 0, 0, 0, 32, 0, 0, 0, 8, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, 0, 96, 46, 114, 101, 108, 111, 99, 0, 0, 12, 0, 0, 0, 0, 64, 0, 0, 0, 2, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 0, 0, 66, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 144, 39, 0, 0, 0, 0, 0, 0, 72, 0, 0, 0, 2, 0, 5, 0, 24, 33, 0, 0, 68, 6, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 19, 48, 2, 0, 56, 0, 0, 0, 1, 0, 0, 17, 115, 3, 0, 0, 6, 10, 6, 2, 125, 3, 0, 0, 4, 6, 40, 10, 0, 0, 10, 125, 2, 0, 0, 4, 6, 21, 125, 1, 0, 0, 4, 6, 124, 2, 0, 0, 4, 18, 0, 40, 1, 0, 0, 43, 6, 124, 2, 0, 0, 4, 40, 12, 0, 0, 10, 42, 34, 2, 40, 13, 0, 0, 10, 0, 42, 0, 0, 0, 27, 48, 2, 0, 88, 0, 0, 0, 2, 0, 0, 17, 2, 123, 1, 0, 0, 4, 10, 0, 114, 1, 0, 0, 112, 40, 14, 0, 0, 10, 140, 15, 0, 0, 1, 40, 15, 0, 0, 10, 40, 16, 0, 0, 10, 0, 32, 200, 0, 0, 0, 11, 222, 24, 12, 2, 31, 254, 125, 1, 0, 0, 4, 2, 124, 2, 0, 0, 4, 8, 40, 17, 0, 0, 10, 0, 222, 21, 2, 31, 254, 125, 1, 0, 0, 4, 2, 124, 2, 0, 0, 4, 7, 40, 18, 0, 0, 10, 0, 42, 1, 16, 0, 0, 0, 0, 7, 0, 35, 42, 0, 24, 12, 0, 0, 1, 6, 42, 0, 0, 66, 83, 74, 66, 1, 0, 1, 0, 0, 0, 0, 0, 12, 0, 0, 0, 118, 52, 46, 48, 46, 51, 48, 51, 49, 57, 0, 0, 0, 0, 5, 0, 108, 0, 0, 0, 48, 2, 0, 0, 35, 126, 0, 0, 156, 2, 0, 0, 120, 2, 0, 0, 35, 83, 116, 114, 105, 110, 103, 115, 0, 0, 0, 0, 20, 5, 0, 0, 40, 0, 0, 0, 35, 85, 83, 0, 60, 5, 0, 0, 16, 0, 0, 0, 35, 71, 85, 73, 68, 0, 0, 0, 76, 5, 0, 0, 248, 0, 0, 0, 35, 66, 108, 111, 98, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 1, 87, 23, 2, 10, 9, 10, 0, 0, 0, 250, 1, 51, 0, 22, 0, 0, 1, 0, 0, 0, 17, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 5, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 18, 0, 0, 0, 7, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 162, 1, 1, 0, 0, 0, 0, 0, 6, 0, 84, 1, 237, 1, 6, 0, 116, 1, 237, 1, 6, 0, 240, 0, 218, 1, 15, 0, 13, 2, 0, 0, 6, 0, 68, 2, 179, 1, 6, 0, 190, 0, 179, 1, 6, 0, 4, 1, 237, 1, 6, 0, 31, 1, 218, 1, 6, 0, 19, 0, 38, 2, 6, 0, 213, 0, 237, 1, 6, 0, 142, 0, 237, 1, 6, 0, 189, 1, 179, 1, 6, 0, 26, 0, 237, 1, 6, 0, 60, 1, 218, 1, 6, 0, 123, 0, 179, 1, 6, 0, 146, 1, 179, 1, 6, 0, 115, 0, 179, 1, 0, 0, 0, 0, 64, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 16, 0, 85, 2, 94, 0, 21, 0, 1, 0, 1, 0, 3, 1, 16, 0, 1, 0, 0, 0, 21, 0, 1, 0, 3, 0, 6, 0, 202, 0, 114, 0, 6, 0, 199, 1, 117, 0, 6, 0, 28, 2, 124, 0, 80, 32, 0, 0, 0, 0, 134, 0, 82, 0, 128, 0, 1, 0, 148, 32, 0, 0, 0, 0, 134, 24, 212, 1, 6, 0, 1, 0, 148, 32, 0, 0, 0, 0, 134, 24, 212, 1, 6, 0, 1, 0, 160, 32, 0, 0, 0, 0, 225, 1, 102, 2, 6, 0, 1, 0, 20, 33, 0, 0, 0, 0, 225, 1, 161, 0, 22, 0, 1, 0, 0, 0, 1, 0, 177, 0, 3, 0, 45, 0, 9, 0, 212, 1, 1, 0, 17, 0, 212, 1, 6, 0, 25, 0, 212, 1, 10, 0, 57, 0, 212, 1, 16, 0, 65, 0, 212, 1, 6, 0, 81, 0, 212, 1, 6, 0, 89, 0, 102, 2, 6, 0, 89, 0, 161, 0, 22, 0, 113, 0, 212, 1, 6, 0, 12, 0, 195, 0, 39, 0, 12, 0, 96, 2, 48, 0, 12, 0, 153, 1, 61, 0, 41, 0, 212, 1, 6, 0, 121, 0, 111, 2, 77, 0, 129, 0, 61, 2, 82, 0, 137, 0, 132, 0, 88, 0, 12, 0, 186, 1, 93, 0, 12, 0, 75, 2, 99, 0, 32, 0, 35, 0, 185, 0, 32, 0, 43, 0, 240, 0, 46, 0, 11, 0, 136, 0, 46, 0, 19, 0, 145, 0, 46, 0, 27, 0, 176, 0, 99, 0, 51, 0, 240, 0, 160, 0, 75, 0, 240, 0, 28, 0, 70, 0, 3, 0, 8, 0, 15, 0, 3, 0, 10, 0, 17, 0, 33, 0, 4, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 51, 0, 0, 0, 2, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 105, 0, 73, 0, 0, 0, 0, 0, 3, 0, 2, 0, 23, 0, 56, 0, 0, 0, 0, 60, 65, 99, 116, 105, 111, 110, 65, 115, 121, 110, 99, 62, 100, 95, 95, 48, 0, 84, 97, 115, 107, 96, 49, 0, 65, 115, 121, 110, 99, 84, 97, 115, 107, 77, 101, 116, 104, 111, 100, 66, 117, 105, 108, 100, 101, 114, 96, 49, 0, 103, 100, 104, 101, 52, 49, 55, 97, 46, 102, 111, 57, 0, 60, 77, 111, 100, 117, 108, 101, 62, 0, 109, 115, 99, 111, 114, 108, 105, 98, 0, 65, 99, 116, 105, 111, 110, 65, 115, 121, 110, 99, 0, 68, 117, 110, 103, 101, 111, 110, 66, 111, 116, 46, 71, 101, 110, 101, 114, 97, 116, 101, 100, 0, 67, 111, 110, 115, 111, 108, 101, 0, 68, 97, 116, 101, 84, 105, 109, 101, 0, 87, 114, 105, 116, 101, 76, 105, 110, 101, 0, 73, 65, 115, 121, 110, 99, 83, 116, 97, 116, 101, 77, 97, 99, 104, 105, 110, 101, 0, 83, 101, 116, 83, 116, 97, 116, 101, 77, 97, 99, 104, 105, 110, 101, 0, 115, 116, 97, 116, 101, 77, 97, 99, 104, 105, 110, 101, 0, 84, 121, 112, 101, 0, 67, 114, 101, 97, 116, 101, 0, 60, 62, 49, 95, 95, 115, 116, 97, 116, 101, 0, 67, 111, 109, 112, 105, 108, 101, 114, 71, 101, 110, 101, 114, 97, 116, 101, 100, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 68, 101, 98, 117, 103, 103, 97, 98, 108, 101, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 65, 115, 121, 110, 99, 83, 116, 97, 116, 101, 77, 97, 99, 104, 105, 110, 101, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 68, 101, 98, 117, 103, 103, 101, 114, 83, 116, 101, 112, 84, 104, 114, 111, 117, 103, 104, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 68, 101, 98, 117, 103, 103, 101, 114, 72, 105, 100, 100, 101, 110, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 67, 111, 109, 112, 105, 108, 97, 116, 105, 111, 110, 82, 101, 108, 97, 120, 97, 116, 105, 111, 110, 115, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 82, 117, 110, 116, 105, 109, 101, 67, 111, 109, 112, 97, 116, 105, 98, 105, 108, 105, 116, 121, 65, 116, 116, 114, 105, 98, 117, 116, 101, 0, 83, 116, 114, 105, 110, 103, 0, 103, 101, 116, 95, 84, 97, 115, 107, 0, 103, 100, 104, 101, 52, 49, 55, 97, 46, 102, 111, 57, 46, 100, 108, 108, 0, 83, 121, 115, 116, 101, 109, 0, 83, 101, 116, 69, 120, 99, 101, 112, 116, 105, 111, 110, 0, 60, 62, 116, 95, 95, 98, 117, 105, 108, 100, 101, 114, 0, 46, 99, 116, 111, 114, 0, 83, 121, 115, 116, 101, 109, 46, 68, 105, 97, 103, 110, 111, 115, 116, 105, 99, 115, 0, 83, 121, 115, 116, 101, 109, 46, 82, 117, 110, 116, 105, 109, 101, 46, 67, 111, 109, 112, 105, 108, 101, 114, 83, 101, 114, 118, 105, 99, 101, 115, 0, 68, 101, 98, 117, 103, 103, 105, 110, 103, 77, 111, 100, 101, 115, 0, 60, 62, 52, 95, 95, 116, 104, 105, 115, 0, 83, 121, 115, 116, 101, 109, 46, 84, 104, 114, 101, 97, 100, 105, 110, 103, 46, 84, 97, 115, 107, 115, 0, 70, 111, 114, 109, 97, 116, 0, 79, 98, 106, 101, 99, 116, 0, 83, 101, 116, 82, 101, 115, 117, 108, 116, 0, 68, 117, 110, 103, 101, 111, 110, 66, 111, 116, 0, 83, 116, 97, 114, 116, 0, 77, 111, 118, 101, 78, 101, 120, 116, 0, 103, 101, 116, 95, 78, 111, 119, 0, 0, 0, 37, 123, 0, 48, 0, 125, 0, 32, 0, 45, 0, 32, 0, 72, 0, 101, 0, 108, 0, 108, 0, 111, 0, 32, 0, 87, 0, 111, 0, 114, 0, 108, 0, 100, 0, 33, 0, 1, 0, 52, 221, 61, 226, 169, 22, 42, 66, 181, 69, 205, 61, 61, 164, 213, 183, 0, 4, 32, 1, 1, 8, 3, 32, 0, 1, 5, 32, 1, 1, 17, 17, 5, 32, 1, 1, 18, 25, 5, 32, 1, 1, 18, 45, 4, 7, 1, 18, 12, 5, 21, 17, 53, 1, 8, 8, 0, 0, 21, 17, 53, 1, 19, 0, 7, 48, 1, 1, 1, 16, 30, 0, 4, 10, 1, 18, 12, 8, 32, 0, 21, 18, 37, 1, 19, 0, 6, 7, 3, 8, 8, 18, 49, 4, 0, 0, 17, 61, 5, 0, 2, 14, 14, 28, 4, 0, 1, 1, 14, 5, 32, 1, 1, 18, 49, 5, 32, 1, 1, 19, 0, 8, 124, 236, 133, 215, 190, 167, 121, 142, 2, 6, 8, 6, 6, 21, 17, 53, 1, 8, 3, 6, 18, 8, 7, 32, 0, 21, 18, 37, 1, 8, 8, 1, 0, 8, 0, 0, 0, 0, 0, 30, 1, 0, 1, 0, 84, 2, 22, 87, 114, 97, 112, 78, 111, 110, 69, 120, 99, 101, 112, 116, 105, 111, 110, 84, 104, 114, 111, 119, 115, 1, 8, 1, 0, 7, 1, 0, 0, 0, 0, 54, 1, 0, 49, 68, 117, 110, 103, 101, 111, 110, 66, 111, 116, 46, 71, 101, 110, 101, 114, 97, 116, 101, 100, 46, 68, 117, 110, 103, 101, 111, 110, 66, 111, 116, 43, 60, 65, 99, 116, 105, 111, 110, 65, 115, 121, 110, 99, 62, 100, 95, 95, 48, 0, 0, 4, 1, 0, 0, 0, 0, 0, 0, 132, 39, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 158, 39, 0, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 144, 39, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 95, 67, 111, 114, 68, 108, 108, 77, 97, 105, 110, 0, 109, 115, 99, 111, 114, 101, 101, 46, 100, 108, 108, 0, 0, 0, 0, 0, 255, 37, 0, 32, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, 0, 12, 0, 0, 0, 176, 55, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    new ActionModuleFile("DungeonBot.cs", @"using System;
using System.Threading.Tasks;
using DungeonBot;

namespace DungeonBot.Scripts
{
    public class AttackActionModule
    {
        [ActionModuleEntrypoint]
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            return actionComponent.Attack(sensorComponent.Enemy);
        }
    }
}"))
            });
        }
    }
}
