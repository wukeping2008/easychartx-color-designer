#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import markdown
from reportlab.lib.pagesizes import A4
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.units import mm
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, PageBreak, Table, TableStyle
from reportlab.lib.colors import HexColor, black, blue, gray
from reportlab.lib.enums import TA_CENTER, TA_LEFT, TA_JUSTIFY
from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont
import re
import os

def setup_fonts():
    """设置中文字体"""
    try:
        # 尝试使用系统中文字体
        font_paths = [
            '/System/Library/Fonts/PingFang.ttc',  # macOS
            '/System/Library/Fonts/STHeiti Light.ttc',  # macOS 黑体
            '/System/Library/Fonts/Helvetica.ttc',  # macOS fallback
            'C:/Windows/Fonts/msyh.ttc',  # Windows 微软雅黑
            'C:/Windows/Fonts/simhei.ttf',  # Windows 黑体
            'C:/Windows/Fonts/simsun.ttc',  # Windows 宋体
            '/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf'  # Linux
        ]
        
        for font_path in font_paths:
            if os.path.exists(font_path):
                try:
                    pdfmetrics.registerFont(TTFont('ChineseFont', font_path))
                    print(f"成功加载字体: {font_path}")
                    return 'ChineseFont'
                except Exception as e:
                    print(f"字体加载失败 {font_path}: {e}")
                    continue
        
        # 如果没有找到合适的字体，使用默认字体
        print("警告: 未找到中文字体，使用默认字体")
        return 'Helvetica'
    except Exception as e:
        print(f"字体设置错误: {e}")
        return 'Helvetica'

def create_styles(font_name):
    """创建样式"""
    styles = getSampleStyleSheet()
    
    # 标题样式
    styles.add(ParagraphStyle(
        name='CustomTitle',
        parent=styles['Title'],
        fontName=font_name,
        fontSize=24,
        spaceAfter=20,
        alignment=TA_CENTER,
        textColor=HexColor('#2c3e50')
    ))
    
    # 副标题样式
    styles.add(ParagraphStyle(
        name='CustomSubtitle',
        parent=styles['Normal'],
        fontName=font_name,
        fontSize=16,
        spaceAfter=15,
        alignment=TA_CENTER,
        textColor=HexColor('#666666')
    ))
    
    # 章节标题样式
    styles.add(ParagraphStyle(
        name='CustomHeading1',
        parent=styles['Heading1'],
        fontName=font_name,
        fontSize=18,
        spaceAfter=12,
        spaceBefore=20,
        textColor=HexColor('#34495e'),
        leftIndent=0
    ))
    
    # 二级标题样式
    styles.add(ParagraphStyle(
        name='CustomHeading2',
        parent=styles['Heading2'],
        fontName=font_name,
        fontSize=14,
        spaceAfter=10,
        spaceBefore=15,
        textColor=HexColor('#2c3e50'),
        leftIndent=0
    ))
    
    # 三级标题样式
    styles.add(ParagraphStyle(
        name='CustomHeading3',
        parent=styles['Heading3'],
        fontName=font_name,
        fontSize=12,
        spaceAfter=8,
        spaceBefore=12,
        textColor=HexColor('#34495e'),
        leftIndent=0
    ))
    
    # 正文样式
    styles.add(ParagraphStyle(
        name='CustomBody',
        parent=styles['Normal'],
        fontName=font_name,
        fontSize=11,
        spaceAfter=8,
        alignment=TA_JUSTIFY,
        textColor=black,
        leading=14
    ))
    
    # 代码样式
    styles.add(ParagraphStyle(
        name='CustomCode',
        parent=styles['Code'],
        fontName='Courier',
        fontSize=9,
        spaceAfter=10,
        spaceBefore=10,
        backColor=HexColor('#f8f9fa'),
        borderColor=HexColor('#e9ecef'),
        borderWidth=1,
        borderPadding=8,
        leading=11
    ))
    
    # 列表样式
    styles.add(ParagraphStyle(
        name='CustomBullet',
        parent=styles['Normal'],
        fontName=font_name,
        fontSize=11,
        spaceAfter=4,
        leftIndent=20,
        bulletIndent=10,
        textColor=black,
        leading=14
    ))
    
    return styles

def clean_text(text):
    """清理文本，确保PDF兼容性"""
    if not text:
        return ""
    
    # 保留中文字符、英文字符、数字和常用标点符号
    # 移除可能导致PDF显示问题的特殊字符和emoji
    cleaned = re.sub(r'[^\w\s\u4e00-\u9fff\-\(\)（）：:、，。！？；;""''""《》【】\[\]{}\.\/\\#@&%\+\=\*\^\$\|~`]', '', text)
    
    # 处理特殊的markdown符号
    cleaned = cleaned.replace('⭐', '★')  # 替换星号emoji
    cleaned = cleaned.replace('✨', '★')
    cleaned = cleaned.replace('🎯', '●')
    cleaned = cleaned.replace('📖', '●')
    cleaned = cleaned.replace('🏆', '●')
    cleaned = cleaned.replace('📊', '●')
    cleaned = cleaned.replace('🎨', '●')
    cleaned = cleaned.replace('🔧', '●')
    cleaned = cleaned.replace('📤', '●')
    cleaned = cleaned.replace('🚀', '●')
    cleaned = cleaned.replace('📋', '●')
    cleaned = cleaned.replace('🔄', '●')
    cleaned = cleaned.replace('🎯', '●')
    cleaned = cleaned.replace('🔬', '●')
    cleaned = cleaned.replace('🆘', '●')
    cleaned = cleaned.replace('📝', '●')
    cleaned = cleaned.replace('📞', '●')
    
    return cleaned.strip()

def parse_markdown_to_elements(md_content, styles):
    """解析Markdown内容为PDF元素"""
    elements = []
    lines = md_content.split('\n')
    
    i = 0
    while i < len(lines):
        line = lines[i].strip()
        
        if not line:
            i += 1
            continue
            
        # 处理标题
        if line.startswith('# '):
            title = clean_text(line[2:].strip())
            if '波形配色方案设计器' in title:
                elements.append(Paragraph(title, styles['CustomTitle']))
                elements.append(Spacer(1, 10*mm))
                elements.append(Paragraph('功能说明和操作指南', styles['CustomSubtitle']))
                elements.append(PageBreak())
            else:
                elements.append(Paragraph(title, styles['CustomHeading1']))
                
        elif line.startswith('## '):
            title = clean_text(line[3:].strip())
            elements.append(Paragraph(title, styles['CustomHeading1']))
            
        elif line.startswith('### '):
            title = clean_text(line[4:].strip())
            elements.append(Paragraph(title, styles['CustomHeading2']))
            
        elif line.startswith('#### '):
            title = clean_text(line[5:].strip())
            elements.append(Paragraph(title, styles['CustomHeading3']))
            
        # 处理代码块
        elif line.startswith('```'):
            i += 1
            code_lines = []
            while i < len(lines) and not lines[i].strip().startswith('```'):
                code_lines.append(lines[i])
                i += 1
            if code_lines:
                # 处理代码内容，保持原始格式
                code_text = '\n'.join(code_lines)
                # 转义HTML特殊字符
                code_text = code_text.replace('&', '&amp;')
                code_text = code_text.replace('<', '&lt;')
                code_text = code_text.replace('>', '&gt;')
                
                # 分割长行以适应PDF页面
                formatted_code = []
                for code_line in code_lines:
                    if len(code_line) > 80:
                        while len(code_line) > 80:
                            formatted_code.append(code_line[:80])
                            code_line = '    ' + code_line[80:]
                        if code_line.strip():
                            formatted_code.append(code_line)
                    else:
                        formatted_code.append(code_line)
                
                code_text = '\n'.join(formatted_code)
                try:
                    elements.append(Paragraph(code_text.replace('\n', '<br/>'), styles['CustomCode']))
                except Exception as e:
                    print(f"代码块处理错误: {e}")
                    # 如果代码块处理失败，添加简化版本
                    elements.append(Paragraph("代码示例（请参考原文档）", styles['CustomBody']))
                
        # 处理列表
        elif line.startswith('- ') or line.startswith('* '):
            text = clean_text(line[2:].strip())
            # 处理markdown格式
            text = re.sub(r'\*\*(.*?)\*\*', r'<b>\1</b>', text)  # 粗体
            text = re.sub(r'`(.*?)`', r'<font name="Courier">\1</font>', text)  # 代码
            try:
                elements.append(Paragraph(f'• {text}', styles['CustomBullet']))
            except Exception as e:
                print(f"列表项处理错误: {e}")
                elements.append(Paragraph(f'• {text}', styles['CustomBody']))
            
        # 处理有序列表
        elif re.match(r'^\d+\. ', line):
            text = clean_text(re.sub(r'^\d+\. ', '', line).strip())
            text = re.sub(r'\*\*(.*?)\*\*', r'<b>\1</b>', text)
            try:
                elements.append(Paragraph(text, styles['CustomBullet']))
            except Exception as e:
                print(f"有序列表处理错误: {e}")
                elements.append(Paragraph(text, styles['CustomBody']))
            
        # 跳过分隔线和其他特殊行
        elif line.startswith('---') or line.startswith('==='):
            elements.append(Spacer(1, 5*mm))
            
        # 处理普通段落
        else:
            if line and not line.startswith('**版本信息**') and not line.startswith('**更新日期**'):
                # 清理文本
                text = clean_text(line)
                if text:  # 只有在清理后还有内容时才添加
                    # 处理markdown格式
                    text = re.sub(r'\*\*(.*?)\*\*', r'<b>\1</b>', text)  # 粗体
                    text = re.sub(r'`(.*?)`', r'<font name="Courier">\1</font>', text)  # 代码
                    try:
                        elements.append(Paragraph(text, styles['CustomBody']))
                    except Exception as e:
                        print(f"段落处理错误: {e}, 文本: {text[:50]}...")
                        # 如果处理失败，尝试更简单的版本
                        simple_text = re.sub(r'[^\w\s\u4e00-\u9fff]', '', text)
                        if simple_text:
                            elements.append(Paragraph(simple_text, styles['CustomBody']))
        
        i += 1
    
    return elements

def generate_pdf_from_markdown():
    """从Markdown生成PDF"""
    try:
        # 读取README.md文件
        with open('README.md', 'r', encoding='utf-8') as f:
            md_content = f.read()
        
        # 设置字体
        font_name = setup_fonts()
        
        # 创建样式
        styles = create_styles(font_name)
        
        # 创建PDF文档
        filename = 'EasyChartX波形配色方案设计器-功能说明和操作指南.pdf'
        doc = SimpleDocTemplate(
            filename,
            pagesize=A4,
            rightMargin=20*mm,
            leftMargin=20*mm,
            topMargin=20*mm,
            bottomMargin=20*mm
        )
        
        # 解析Markdown内容
        elements = parse_markdown_to_elements(md_content, styles)
        
        # 生成PDF
        doc.build(elements)
        
        print(f'PDF生成成功：{filename}')
        return True
        
    except Exception as e:
        print(f'PDF生成失败：{str(e)}')
        return False

if __name__ == '__main__':
    generate_pdf_from_markdown()
