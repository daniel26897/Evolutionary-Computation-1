﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    class DrawTree
    {
        private const int NODE_DIAMETER = 22;
        private const int TREE_LEVEL_MARGIN = 50;
        private const int TEXT_SIZE = 14;

        Node rootNode;
        Color nodeColor = Color.Blue;
        int indentation = 0;

        Size nodeRadius = new Size(NODE_DIAMETER, NODE_DIAMETER);
        Form treeForm;
        Graphics g;

        public DrawTree(Tree mainTree)
        {
            rootNode = mainTree.getRoot();
            treeForm = new Form();
            treeForm.WindowState = FormWindowState.Maximized;
            g = treeForm.CreateGraphics();
            Point frameBegin = new Point(0, 0);
            Point frameEnd = new Point(treeForm.Size.Width, 0);

           // printTreeConsole(rootNode);                  // Print tree to Console

            treeForm.Show();
            DrawTreeGUI(frameBegin, frameEnd, rootNode); // Draw Tree in GUI
            g.Dispose();
        }

        private Point DrawTreeGUI(Point frameBegin, Point frameEnd, Node currentNode)
        {
            Point nodeLocation = new Point();
            int frameWidth = frameEnd.X - frameBegin.X;
            string nodeDescription = currentNode.toString();
            int numOfChildren = currentNode.getNumChildren();

            nodeLocation.X = frameBegin.X + frameWidth / 2;
            nodeLocation.Y = frameBegin.Y;

            // Draw currentNode
            DrawEllipse(nodeLocation, nodeRadius, nodeColor);
            DrawString(nodeDescription, nodeLocation, TEXT_SIZE);

            if (numOfChildren > 0)
            {
                int newFrameWidth = frameWidth / numOfChildren;

                // Draw node's Children
                for (int i = 0; i < numOfChildren; i++)
                {
                    Node newNode = currentNode.getChildAtIndex(i);
                    Point newFrameBegin = new Point();
                    Point newFrameEnd = new Point();

                    newFrameBegin.X = frameBegin.X + newFrameWidth * i;
                    newFrameEnd.X = newFrameBegin.X + newFrameWidth;

                    newFrameBegin.Y = frameBegin.Y + TREE_LEVEL_MARGIN;
                    newFrameEnd.Y = frameBegin.Y + TREE_LEVEL_MARGIN;

                    Point childLocation = DrawTreeGUI(newFrameBegin, newFrameEnd, newNode); // Draw child and get it location
                    DrawLine(nodeLocation, childLocation);                                   // Draw line to child
                }
            }
            return nodeLocation;
        }

        private void DrawEllipse(Point position, Size radius, Color color)
        {
            SolidBrush brush = new SolidBrush(color);
            Rectangle circle = new Rectangle(position, radius);

            g.FillEllipse(brush, circle);
        }

        private void DrawLine(Point begin, Point end)
        {
            Pen blackPen = new Pen(Color.Black, 1);

            // Correct points
            begin.X += NODE_DIAMETER / 2;
            begin.Y += NODE_DIAMETER;

            end.X += NODE_DIAMETER / 2;

            g.DrawLine(blackPen, begin, end);
        }

        public void DrawString(string description, Point position, int size)
        {
            Font drawFont = new Font("Arial", size);
            SolidBrush drawBrush = new SolidBrush(Color.Red);
            PointF drawPoint = new PointF(150.0F, 150.0F);

            description = description.IndexOf(" ") > -1 ? description.Substring(0, description.IndexOf(" ")) : description;

            switch (description)
            {
                case "If":
                    description = "If";
                    break;
                case "NeighborsAmount":
                    description = "na";
                    break;
                case "RowAmount":
                    description = "ra";
                    break;
                case "ColumnAmount":
                    description = "ca";
                    break;
                case "RowStreak":
                    description = "rs";
                    break;
                case "ColumnStreak":
                    description = "cs";
                    break;
                case "PrimaryDiagStreak":
                    description = "pds";
                    break;
                case "SecDiagStreak":
                    description = "sds";
                    break;
                case "RandVal":
                    description = "rv";
                    break;
                case "WinMove":
                    description = "wm";
                    break;
                case "LoseMove":
                    description = "lm";
                    break;
                case "If <=":
                    description = "<=";
                    break;
                case "If >=":
                    description = ">=";
                    break;
                case "Minus":
                    description = " -";
                    break;
                case "Plus":
                    description = " +";
                    break;
                case "Multi":
                    description = " *";
                    break;
                default:
                    Console.WriteLine("Unknown Node description: {0}", description);
                    description = " ?";
                    break;
            }

            Graphics g = treeForm.CreateGraphics();
            g.DrawString(description, drawFont, drawBrush, position);
            g.Dispose();
        }

        private void printTreeConsole(Node currentNode)
        {
            int numOfChildren = currentNode.getNumChildren();

            for (int i = 0; i < indentation; i++)
                Console.Write("\t");
            Console.WriteLine(currentNode.toString());

            indentation++;
            for (int i = 0; i < numOfChildren; i++)
            {
                printTreeConsole(currentNode.getChildAtIndex(i));
            }
            indentation--;
        }
    }
}
