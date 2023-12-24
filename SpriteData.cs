using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;


public class SpriteData
{
    private int currentAtlasId = 0;
    public List<Texture2D> spriteAtlas;                                                 //������ ��������
    public Texture2D CurrentSpriteAtlas { get { return spriteAtlas[currentAtlasId]; } } //����� ����� � ������������� � ������ ������ ���������������
    private int currentSpriteID = 0;                                                    //����� ������� � ������ �������� ������, ������� ������������ ��� ������������� � ������ ������ �������
    public List<int> Rows { get; set; }                                                 //���������� �����
    public List<int> Columns { get; set; }                                              //���������� ��������
    private List<int> totalFrames = new List<int>();                                    //����� ���������� ������ � ������ ������ (������*�������)
    public List<int> framesPerSecond = new List<int>();
    private int tickCounter = 0;
    public bool animationIsPaused = true;

    public int widthOfFrame;
    public int heightOfFrame;
    public int currentRow;
    public int currentColumn;

    public Rectangle sourceRectangleOfFrame;

    public SpriteData(List<Texture2D> initSprite, List<int> _rows, List<int> _columns, List<int> animationSpeed)
    {
        spriteAtlas = initSprite;
        Rows = _rows;
        Columns = _columns;

        for (int i = 0; i < spriteAtlas.Count; i++)
        {
            totalFrames.Add(_rows[i] * _columns[i]);
        }
        framesPerSecond = animationSpeed;
        UpdateFrame();
    }

    public void Update()
    {   
        if (framesPerSecond[currentAtlasId] > 0 && !animationIsPaused) //��� ������ ��������� ��������, ���� ��� �����������.
        {
            tickCounter++;
            if (tickCounter > 60 / framesPerSecond[currentAtlasId])
            {
                tickCounter = 0;
                currentSpriteID = (currentSpriteID + 1) % totalFrames[currentAtlasId];
                UpdateFrame();
            }
        }
    }

    private void UpdateFrame() //��������� ������ ����� (�������, ������� ���������� ���������� �������� �� ����� ������)
    {
        widthOfFrame = spriteAtlas[currentAtlasId].Width / Columns[currentAtlasId];
        heightOfFrame = spriteAtlas[currentAtlasId].Height / Rows[currentAtlasId]; 
        currentRow = currentSpriteID / Columns[currentAtlasId];
        currentColumn = currentSpriteID % Columns[currentAtlasId];

        sourceRectangleOfFrame = new Rectangle(widthOfFrame * currentColumn, heightOfFrame * currentRow, widthOfFrame, heightOfFrame);
    }

    public void SwitchAnimation(int indexOfNewAtlasAnimation)
    {
        currentAtlasId = indexOfNewAtlasAnimation;
        UpdateFrame();
    }

    public void SwitchAnimationPause(bool newValue)
    {   
        animationIsPaused = newValue;
    }

    public void ResetAnimation() 
    {
        currentSpriteID = 0;
        UpdateFrame();
    }
}
