using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;


public class SpriteData
{
    public Texture2D spriteAtlas;                                                 
    private int currentSpriteID = 0;                                     //����� ������� � ������ �������� ������, ������� ������������ ��� ������������� � ������ ������ �������
    public int Rows { get; set; }                                      //���������� �����
    public int Columns { get; set; }                                //���������� ��������
    private int totalFrames = 0;                                  //����� ���������� ������ (������*�������)
    public int framesPerSecond = 0;
    private int tickCounter = 0;
    public bool animationIsPaused = true;

    public int widthOfFrame;
    public int heightOfFrame;
    public int currentRow;
    public int currentColumn;

    public Rectangle sourceRectangleOfFrame;

    public SpriteData(Texture2D initSprite, int _rows, int _columns, int animationSpeed, int _startSpriteID)
    {
        spriteAtlas = initSprite;
        Rows = _rows;
        Columns = _columns;
        totalFrames = _rows * _columns;
        framesPerSecond = animationSpeed;
        currentSpriteID = _startSpriteID;
        UpdateFrame();
    }

    public void Update()
    {   
        if (framesPerSecond > 0 && !animationIsPaused) //��� ������ ��������� ��������, ���� ��� �����������.
        {
            tickCounter++;
            if (tickCounter > 60 / framesPerSecond)
            {
                tickCounter = 0;
                currentSpriteID = (currentSpriteID + 1) % totalFrames;
                UpdateFrame();
            }
        }
    }

    private void UpdateFrame() //��������� ������ ����� (�������, ������� ���������� ���������� �������� �� ����� ������)
    {
        widthOfFrame = spriteAtlas.Width / Columns;
        heightOfFrame = spriteAtlas.Height / Rows; 
        currentRow = currentSpriteID / Columns;
        currentColumn = currentSpriteID % Columns;

        sourceRectangleOfFrame = new Rectangle(widthOfFrame * currentColumn, heightOfFrame * currentRow, widthOfFrame, heightOfFrame);
    }

    public void SwitchAnimationPause(bool newValue)
    {   
        animationIsPaused = newValue;
    }

    public void SetFrame(int _spriteID)
    {
        currentSpriteID = _spriteID;
        UpdateFrame();
    }

    public void ResetAnimation() 
    {
        currentSpriteID = 0;
        UpdateFrame();
    }
}
